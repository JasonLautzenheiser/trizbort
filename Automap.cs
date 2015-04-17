/*
    Copyright (c) 2010-2015 by Genstein and Jason Lautzenheiser.

    This file is (or was originally) part of Trizbort, the Interactive Fiction Mapper.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Trizbort
{
    class Automap : IDisposable
    {
        public Automap(IAutomapCanvas canvas, AutomapSettings settings)
        {
            m_canvas = canvas;
            m_settings = settings;
            Debug.Assert(m_settings.AssumeRoomsWithSameNameAreSameRoom || m_settings.VerboseTranscript, "Must assume rooms with same name are same room unless transcript is verbose.");

            m_thread = new Thread(ThreadMain);
            m_thread.Start();
            Status = "Automapping has started.";
        }

        public void Dispose()
        {
            if (m_thread != null)
            {
                m_quit = true;
                Trace("Automap: Set quit and wait.");

                // Thread.Join(), except without the deadlock (sigh).
                var startTime = DateTime.Now;
                do
                {
                    Application.DoEvents();
                    Thread.Sleep(0);
                } while (m_thread.IsAlive && DateTime.Now - startTime < TimeSpan.FromSeconds(10));
                if (m_thread.IsAlive)
                {
                    Trace("Automap: Aborting thread.");
                    m_thread.Abort();
                    m_thread.Join();
                    Status = "Automapping has been aborted.";
                }

                m_thread = null;
            }
        }

        void WaitForStep()
        {
            // for diagnostic purposes, allow single stepping
            if (m_settings.SingleStep && !m_stepNow)
            {
                Status = "Automapping is waiting for you to step through it.";
                while (!m_stepNow && !m_quit)
                {
                    Thread.Sleep(50);
                }
                m_stepNow = false;
            }
        }

        void ThreadMain()
        {
            bool sleep = false;
            while (!m_quit)
            {
                if (sleep)
                {
                    Trace("Automap: Zzz.");
                    Thread.Sleep(1000);
                    sleep = false;

                    if (m_quit)
                    {
                        break;
                    }
                }

                Stream stream;
                try
                {
                    stream = File.Open(m_settings.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                }
                catch (Exception)
                {
                    // couldn't open the file; try again momentarily
                    // TODO: alert the user as well in case they want to stop automapping
                    Trace("Automap: Error opening file.");
                    Status = "Automapping cannot open the transcript but will keep trying.";
                    sleep = true;
                    continue;
                }

                using (stream)
                {
                    StreamReader reader;
                    try
                    {
                        reader = new StreamReader(stream);
                    }
                    catch (Exception)
                    {
                        // couldn't read from the file; try again momentarily
                        // TODO: alert the user as well in case they want to stop automapping
                        Trace("Automap: Error reading from file.");
                        Status = "Automapping cannot read the transcript but will keep trying.";
                        sleep = true;
                        continue;
                    }

                    using (reader)
                    {
                        // keep track of lines we read between here and the next prompt
                        var linesBetweenPrompts = new List<string>();
                        var nextLineIndex = 0;

                        try
                        {
                            // skip by line to where we want to be next
                            // (We could do this with file pointers, but they don't sit well with StreamReader;
                            // We can't safely read by byte since we want to treat the file as properly encoded text.
                            // There may be much better way of doing this. Whatever it is, we don't use it.)
                            while (nextLineIndex < m_nextLineIndexToRead && !reader.EndOfStream)
                            {
                                reader.ReadLine();
                                ++nextLineIndex;
                            }
                        }
                        catch (Exception)
                        {
                            // couldn't read from the file; try again momentarily
                            // TODO: alert the user as well in case they want to stop automapping
                            Trace("Automap: Error skipping already ready lines in file.");
                            Status = "Automapping could not skip already read lines, but will keep trying.";
                            sleep = true;
                            continue;
                        }

                        Status = "Automapping is processing the transcript.";

                        var promptLine = string.Empty;
                        // while we've got text left to read in the file...
                        while (!reader.EndOfStream)
                        {
                            // ...read a line of text
                            string line;
                            try
                            {
                                line = reader.ReadLine();
                            }
                            catch (Exception)
                            {
                                // couldn't read from the file; try again momentarily
                                // TODO: alert the user as well in case they want to stop automapping
                                Trace("Automap: Error reading line in file.");
                                Status = "Automapping could read lines from the transcript, but will keep trying.";
                                sleep = true;
                                break;
                            }
                            ++nextLineIndex;

                            //Trace("[" + line + "]");
                            string command;
                            if (IsPrompt(line, out command))
                            {
                                // this is a prompt line

                                // let's process everything leading up to it since the last prompt, but not necessarily this new prompt itself
                                ProcessTranscriptText(linesBetweenPrompts);

                                // we've now dealt with all lines to this point
                                linesBetweenPrompts.Clear();

                                // if prompts are at the end of the stream, don't process them yet;
                                // they're probably not complete. wait until there's something after them.
                                if (!reader.EndOfStream)
                                {
                                    // process this prompt since it's not at the end of the stream
                                    ProcessPromptCommand(line);

                                    Trace("{0}: {1}{2}", FormatTranscriptLineForDisplay(line), m_lastMoveDirection != null ? "GO " : string.Empty, m_lastMoveDirection != null ? m_lastMoveDirection.Value.ToString().ToUpperInvariant() : string.Empty);

                                    // if we run out of file before hitting the next prompt, we start again after this processed prompt
                                    m_nextLineIndexToRead = nextLineIndex;
                                }
                                else
                                {
                                    // this prompt is at the end of the stream; don't process it yet;
                                    // but we've just run out of file, so next time remember to start with this prompt
                                    m_nextLineIndexToRead = nextLineIndex - 1;
                                }

                                // yield back to our top level loop, mainly in order that we can be shut down gracefully mid-transcript.
                                break;
                            }
                            else
                            {
                                // this line isn't a prompt;
                                // hang onto it for now in case we meet a prompt shortly.
                                linesBetweenPrompts.Add(line);
                            }
                        }

                        // if we hit the end of the stream, we'll sleep momentarily
                        sleep = reader.EndOfStream;
                        Status = "Automapping is waiting for more text.";
                    }
                }
            }
            Trace("Automap: Gentle thread exit.");
            Status = "Automapping has completed.";
        }

        bool IsPrompt(string line, out string typedCommand)
        {
            foreach (var promptMarker in s_promptMarkers)
            {
                var startIndex = line.LastIndexOf(promptMarker);
                if (startIndex != -1 && startIndex < MaxCharactersBeforePrompt)
                {
                    var command = line.Substring(startIndex + promptMarker.Length);
                    typedCommand = command.Trim();
                    return true;
                }
            }
            typedCommand = null;
            return false;
        }
  
        bool ExtractRoomName(string line, string previousLine, out string name)
        {
            name = null;

            string unused;
            if (previousLine != null && previousLine.Trim().Length > 0 && !IsPrompt(previousLine, out unused))
            {
                // the preceeding line, if any, must be blank, or a prompt
                SetFailureReason("the previous line, if any, must be blank or a prompt");
                return false;
            }

            if (string.IsNullOrEmpty(line))
            {
                // blank lines clearly aren't room names
                SetFailureReason("the line is blank");
                return false;
            }

            // look for leading whitespace
            if (line.TrimStart().Length != line.Length)
            {
                // lines which start with whitespace aren't room names
                SetFailureReason("the line starts with whitespace");
                return false;
            }

            // trim whitespace
            line = line.Trim();
            if (line.Length == 0)
            {
                // we just ran out of line
                SetFailureReason("the line only contains whitespace");
                return false;
            }

            if (!char.IsLetterOrDigit(line[line.Length - 1]))
            {
                // the last character of the room name must be a number or a letter
                SetFailureReason("the line didn't end with a letter or a number");
                return false;
            }

            // strip suffixes such as those in "Bedroom, on the bed" or "Bedroom (on the bed)" or "Bedroom - on the bed"
            bool strippedSuffix;
            do
            {
                strippedSuffix = false;
                foreach (var decorativeSuffixMarker in s_roomDecorativeSuffixMarkers)
                {
                    var indexOfMarker = line.IndexOf(decorativeSuffixMarker);
                    if (indexOfMarker >= 0)
                    {
                        var suffixLength = line.Length - indexOfMarker;
                        if (suffixLength > 30)
                        {
                            // this looks more like punctuation in a sentence, not a suffix
                            SetFailureReason("this line looks more like a sentence");
                            return false;
                        }

                        line = line.Substring(0, indexOfMarker);
                        strippedSuffix = true;
                        break;
                    }
                }
            }
            while (strippedSuffix);

            // swallow any leading/trailing whitespace we just made
            line = line.Trim();
            if (line.Length == 0)
            {
                // we just ran out of line
                SetFailureReason("after stripping suffixes from the line, it was blank");
                return false;
            }

            if (!char.IsLetterOrDigit(line[line.Length - 1]))
            {
                // the last character of the room description must be a number or a letter
                SetFailureReason("after stripping suffixes from the line, it didn't end with a letter or a number");
                return false;
            }

            if (!char.IsLetterOrDigit(line[0]))
            {
                // the first character of the room description must be a number or a letter
                SetFailureReason("the line must start with a letter or a number");
                return false;
            }

            if (!StartsWithCapitalOrNonLetter(line))
            {
                // if the first character of the room description is a letter, it must be capitalised
                SetFailureReason("the line starts with a letter, but it isn't capitalised");
                return false;
            }

            // now verify each word of the room name
            var words = line.Split(s_wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 1)
            {
                // we must have some words
                SetFailureReason("there are no words on the line");
                return false;
            }

            var maxWordLength = 0;
            var wordCountWithAllCaps = 0;
            foreach (var word in words)
            {
                if (!IsRoomDescriptionWord(word))
                {
                    // all words must look room description esque for this to be a room name
                    SetFailureReason("the word \"{0}\" doesn't look like a room description word{1}{2}{3}", word, HaveFailureReason() ? " (" : string.Empty, GetFailureReason(), HaveFailureReason() ? ")" : string.Empty);
                    return false;
                }
                if (!StartsWithCapitalOrNonLetter(word) && word.Length >= 4)
                {
                    // all longish words must start with a capital or non letter.
                    SetFailureReason("all words longer than {0} letters, such as \"{1}\", must start with a capital or non letter", 4, word);
                    return false;
                }
                maxWordLength = Math.Max(maxWordLength, word.Length);
                if (IsAllCaps(word))
                {
                    ++wordCountWithAllCaps;
                }
            }

            if (words.Length > 1 && maxWordLength < 3)
            {
                // we must have at least one word over n letters long
                SetFailureReason("there must be at least {0} word(s) over {1} letter(s) long", 1, 3);
                return false;
            }

            if (wordCountWithAllCaps == words.Length)
            {
                // at least one word must not be all caps
                SetFailureReason("at least one word must not be all caps");
                return false;
            }

            ClearFailureReason();
            name = line;
            return true;
        }

        bool IsRoomDescriptionWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                // there must be a word
                SetFailureReason("the word contains no text");
                return false;
            }
            if (!char.IsLetterOrDigit(word[0]))
            {
                // the first character must be a letter or a digit
                SetFailureReason("the word must begin with a letter or a digit");
                return false;
            }
            ClearFailureReason();
            return true;
        }

        bool StartsWithCapitalOrNonLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                // there must be a word
                return false;
            }
            if (char.IsLetter(word[0]) && !char.IsUpper(word[0]))
            {
                // if it starts with a letter, it must start with a capital letter
                return false;
            }
            return true;
        }

        bool IsAllCaps(string word)
        {
            var letterCount = 0;
            var capitalLetterCount = 0;
            foreach (var character in word)
            {
                if (char.IsLetter(character))
                {
                    ++letterCount;
                    if (char.IsUpper(character))
                    {
                        ++capitalLetterCount;
                    }
                }
            }
            return letterCount > 0 && letterCount == capitalLetterCount;
        }

        bool ExtractParagraph(List<string> lines, int lineIndex, out string paragraph)
        {
            paragraph = null;
            while (lineIndex < lines.Count)
            {
                var line = lines[lineIndex].Trim();
                if (line == "[Previous turn undone.]")
                {
                    // Ignore undo reports. This will have the effect of making this room
                    // look like a non-verbose room.
                    ++lineIndex;
                    continue;
                }
                if (line.Length == 0)
                {
                    // we hit a blank line; give up
                    break;
                }

                string unused;
                if (IsPrompt(line, out unused) || ExtractRoomName(line, lineIndex > 0 ? lines[lineIndex - 1] : null, out unused))
                {
                    // we hit a prompt or a room name; give up
                    break;
                }

                if (paragraph == null)
                {
                    paragraph = line;
                }
                else
                {
                    paragraph = string.Format("{0} {1}", paragraph, line);
                }

                if (line.Length < 65)
                {
                    // we hit a short line; assume the end of the paragraph
                    break;
                }

                ++lineIndex;
            }

            return paragraph != null;
        }

        Room FindRoom(string roomName, string roomDescription)
        {
            return m_canvas.FindRoom(roomName, roomDescription, delegate(string n, string d, Room r)
            {
                return Match(r, n, d);
            });
        }

        bool? Match(Room room, string name, string description)
        {
            if (room == null)
            {
                // never match a null room; this shouldn't happen anyway
                return false;
            }
            if (room.Name != name)
            {
                return false;
            }

            if (!m_settings.VerboseTranscript)
            {
                // transcript is not verbose;
                // must assume room with same name is same room,
                // otherwise the user has to disambiguate potentially without descriptions, and very frequently.
                return true;
            }

            if (m_settings.AssumeRoomsWithSameNameAreSameRoom)
            {
                // ignore the description
                return true;
            }

            if (room.MatchDescription(description))
            {
                // the descriptions match; they're the same room
                return true;
            }

            // the descriptions don't match; they may or may not be the same room
            return null;
        }

        void NowInRoom(Room room)
        {
            m_lastKnownRoom = room;
            m_canvas.SelectRoom(room);
        }

        void DeduceExitsFromDescription(Room room, string description)
        {
            if (!m_settings.GuessExits)
            {
                // we're disabled; do nothing
                return;
            }

            if (string.IsNullOrEmpty(description))
            {
                // we don't have a description to work from
                return;
            }

            // lowercase the description
            description = description.ToLowerInvariant();

            // search it for the names of compass directions
            foreach (var pair in s_namesForExitsInRoomDescriptions)
            {
                var directions = pair.Key;
                var namesOfExits = pair.Value;
                foreach (var directionName in namesOfExits)
                {
                    var index = description.IndexOf(directionName);
                    if (index != -1)
                    {
                        // we found one
                        if (index == 0 || !char.IsLetterOrDigit(description[index - 1]))
                        {
                            // it's not the middle/end of a longer word
                            if (index + directionName.Length == description.Length || !char.IsLetterOrDigit(description[index + directionName.Length]))
                            {
                                // it's not the middle/start of a longer word

                                // add all relevant exits
                                foreach (var direction in directions)
                                {
                                    m_canvas.AddExitStub(room, direction);
                                }
                            }
                        }
                    }
                }
            }
        }

        void ProcessTranscriptText(List<string> lines)
        {
            string previousLine = null;
            for (var index=0; index<lines.Count; ++index)
            {
                var line = lines[index];
                string roomName;
                if (ExtractRoomName(line, previousLine, out roomName))
                {
                    string roomDescription;
                    ExtractParagraph(lines, index + 1, out roomDescription);

                    // work out which room the transcript is referring to here, asking them if necessary
                    var room = FindRoom(roomName, roomDescription);
                    if (room == null)
                    {
                        WaitForStep();

                        // new room
                        if (m_lastKnownRoom != null && m_lastMoveDirection != null)
                        {
                            // player moved to new room
                            // if not added already, add room to map; and join it up to the previous one
                            WaitForStep();
                            room = m_canvas.CreateRoom(m_lastKnownRoom, m_lastMoveDirection.Value, roomName);
                            DeduceExitsFromDescription(room, roomDescription);
                            m_canvas.Connect(m_lastKnownRoom, m_lastMoveDirection.Value, room);
                            NowInRoom(room);
                            Trace("{0}: {1} is now {2} from {3}.", FormatTranscriptLineForDisplay(line), roomName, m_lastMoveDirection.Value.ToString().ToLower(), m_lastKnownRoom.Name);
                        }
                        else
                        {
                            // player teleported to new room;
                            // don't connect it up, as we don't know how they got there
                            room = m_canvas.CreateRoom(m_lastKnownRoom, roomName);
                            DeduceExitsFromDescription(room, roomDescription);
                            NowInRoom(room);
                            Trace("{0}: teleported to new room, {1}.", FormatTranscriptLineForDisplay(line), roomName);
                        }
                    }
                    else if (room != m_lastKnownRoom)
                    {
                        // player moved to existing room
                        if (m_lastKnownRoom != null && m_lastMoveDirection != null)
                        {
                            // player moved sensibly; ensure rooms are connected up
                            WaitForStep();
                            m_canvas.Connect(m_lastKnownRoom, m_lastMoveDirection.Value, room);
                            Trace("{0}: {1} is now {2} from {3}.", FormatTranscriptLineForDisplay(line), roomName, m_lastMoveDirection.Value.ToString().ToLower(), m_lastKnownRoom.Name);
                        }
                        else
                        {
                            // player teleported; do nothing.
                        }

                        NowInRoom(room);
                    }
                    else
                    {
                        // player didn't change rooms
                        Trace("{0}: still in {1}.", FormatTranscriptLineForDisplay(line), m_lastKnownRoom.Name);
                    }

                    // add this description if the room doesn't have it already
                    room.AddDescription(roomDescription);

                    // now forget the last movement direction we saw.
                    // we'll still place any rooms we see before we see a movement direction,
                    // but we won't join them up to this room.
                    // we might end up caring if, for example, the user gives multiple commands at one prompt,
                    // or they're moved to one room and then teleported to another.
                    m_lastMoveDirection = null;
                }
                else
                {
                    Trace("{0}: {1}{2}{3}", FormatTranscriptLineForDisplay(line), HaveFailureReason() ? "not a room name because " : string.Empty, GetFailureReason(), HaveFailureReason() ? "." : string.Empty);
                }
                previousLine = line;
            }
        }

        string FormatTranscriptLineForDisplay(string line)
        {
            var displayLine = line;
            const int MaxDisplayLineLength = 60;
            if (displayLine.Length > MaxDisplayLineLength)
            {
                displayLine = displayLine.Substring(0, MaxDisplayLineLength - 3) + "...";
            }
            while (displayLine.Length < MaxDisplayLineLength)
            {
                displayLine += " ";
            }
            return "|" + displayLine;
        }

        void ProcessPromptCommand(string prompt)
        {
            // unless we find one, this command does not involve moving in a given direction
            m_lastMoveDirection = null;

            // trim the prompt down to the actual command
            // TODO: We entirely don't handle "go east. n. s then w." etc. and I don't see an easy way of doing so.
            prompt = prompt.Trim().Trim('>').Trim();

            // split the command into individual words
            var parts = prompt.Split(s_wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                // there's no command left over
                return;
            }

            // strip out words we consider fluff, like "to".
            var words = new List<string>();
            foreach (var word in parts)
            {
                var stripWord = false;
                foreach (var strippable in s_wordsToStripFromCommands)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Compare(word, strippable) == 0)
                    {
                        stripWord = true;
                        break;
                    }
                }
                if (stripWord)
                {
                    continue;
                }

                words.Add(word);
            }

            // if the command starts with a word meaning "go", remove it.
            if (words.Count > 0)
            {
                // the user wants to add the room to a region
                if (!string.IsNullOrEmpty(m_settings.AddRegionCommand) && StringComparer.InvariantCultureIgnoreCase.Compare(words[0], m_settings.AddRegionCommand) == 0)
                {
                    string regionName = string.Empty;

                    for (var index = 1; index < words.Count; ++index)
                    {
                        if (regionName.Length > 0)
                        {
                            regionName += " ";
                        }
                        regionName += words[index];
                    }
                    if (!string.IsNullOrEmpty(regionName) && m_lastKnownRoom != null)
                    {
                        // region already exists, just set the room to it
                        if (Settings.Regions.Find(p => p.RegionName.Equals(regionName,StringComparison.OrdinalIgnoreCase)) == null)
                        {
                            Settings.Regions.Add(new Region {RegionName = regionName, TextColor = Settings.Color[Colors.LargeText], RColor = Settings.Color[Colors.Fill]});
                        }
                        m_lastKnownRoom.Region = regionName;
                    }
                }
                
                if (!string.IsNullOrEmpty(m_settings.AddObjectCommand) && StringComparer.InvariantCultureIgnoreCase.Compare(words[0],m_settings.AddObjectCommand) == 0)
                {
                    // the user wants to add an object to the map
                    string objectName = string.Empty;
                    for (var index = 1; index < words.Count; ++index)
                    {
                        if (objectName.Length > 0)
                        {
                            objectName += " ";
                        }
                        objectName += words[index];
                    }
                    if (!string.IsNullOrEmpty(objectName) && m_lastKnownRoom != null)
                    {
                        if (!string.IsNullOrEmpty(m_lastKnownRoom.Objects))
                        {
                            bool alreadyExists = false;
                            foreach (var line in m_lastKnownRoom.Objects.Replace("\r", string.Empty).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (StringComparer.InvariantCultureIgnoreCase.Compare(line.Trim(), objectName) == 0)
                                {
                                    alreadyExists = true;
                                    break;
                                }
                            }
                            if (!alreadyExists)
                            {
                                m_lastKnownRoom.Objects += "\r\n" + objectName;
                            }
                        }
                        else
                        {
                            m_lastKnownRoom.Objects = objectName;
                        }
                    }
                    return;
                }

                foreach (var wordMeaningGo in s_wordsMeaningGo)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Compare(words[0], wordMeaningGo) == 0)
                    {
                        words.RemoveAt(0);
                        break;
                    }
                }
            }

            // we should have just one word left
            if (words.Count != 1)
            {
                // we have no words left, or more than one
                return;
            }

            // the word we have left is hopefully a direction
            var possibleDirection = words[0];

            // work out which direction it is, if any
            foreach (var pair in s_namesForMovementCommands)
            {
                var direction = pair.Key;
                var wordsForDirection = pair.Value;
                foreach (var wordForDirection in wordsForDirection)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Compare(possibleDirection, wordForDirection) == 0)
                    {
                        // aha, we know which direction it was
                        m_lastMoveDirection = direction;

                        // remove any stub exit in this direction;
                        // we'll either add a proper connection shortly, or they player can't go that way
                        m_canvas.RemoveExitStub(m_lastKnownRoom, m_lastMoveDirection.Value);

                        return;
                    }
                }
            }

            // the word wasn't for a direction after all.
        }

        string s_failureReason = string.Empty;

        [Conditional("DEBUG")]
        void SetFailureReason(string format, params object[] args)
        {
            s_failureReason = string.Format(format, args);
        }

        [Conditional("DEBUG")]
        void ClearFailureReason()
        {
            s_failureReason = string.Empty;
        }

        string GetFailureReason()
        {
            return s_failureReason;
        }

        bool HaveFailureReason()
        {
            return !string.IsNullOrEmpty(s_failureReason);
        }

        public void Step()
        {
            m_stepNow = true;
        }

        public void RunToCompletion()
        {
            m_settings.SingleStep = false;
            Step();
        }

        public string Status
        {
            get;
            private set;
        }

        [Conditional("DEBUG")]
        static void Trace(string format, params object[] args)
        {
            Debug.WriteLine(string.Format(format, args));
        }

        Room m_lastKnownRoom;

        AutomapDirection? m_lastMoveDirection = null;

        private Thread m_thread;
        private volatile bool m_quit;
        private long m_nextLineIndexToRead;

        private IAutomapCanvas m_canvas;

        private AutomapSettings m_settings;
        private volatile bool m_stepNow;

        static readonly char[] s_wordSeparators = new char[] { ' ' };
        static readonly string[] s_roomDecorativeSuffixMarkers = new string[] { ",", "(", "[", "{", " - " };
        static readonly string[] s_promptMarkers = new string[] { ">" };
        const int MaxCharactersBeforePrompt = 42;

        //static readonly string[] s_commandSeparators = { ".", " then " };
        static readonly string[] s_wordsToStripFromCommands = { "the", "a", "to", "on" };
        static readonly string[] s_wordsMeaningGo = { "go", "walk", "move", };

        static Dictionary<AutomapDirection, List<string>> s_namesForMovementCommands = new Dictionary<AutomapDirection, List<string>>()
        {
            { AutomapDirection.North, new List<string>(){ "north", "n", "fore", "f" } },
            { AutomapDirection.South, new List<string>(){ "south", "s", "aft", "a" } },
            { AutomapDirection.East, new List<string>(){ "east", "e", "starboard", "sb" } },
            { AutomapDirection.West, new List<string>() { "west", "w", "port", "p" } },
            { AutomapDirection.NorthEast, new List<string>() { "northeast", "ne" } },
            { AutomapDirection.SouthEast, new List<string>() { "southeast", "se" } },
            { AutomapDirection.SouthWest, new List<string>() { "southwest", "sw" } },
            { AutomapDirection.NorthWest, new List<string>() { "northwest", "nw" } },
            { AutomapDirection.Up, new List<string>() { "up", "u" } },
            { AutomapDirection.Down, new List<string>() { "down", "d" } },
            { AutomapDirection.In, new List<string>() { "in", "inside" } },
            { AutomapDirection.Out, new List<string>() { "out", "outside" } },
        };

        static Dictionary<List<AutomapDirection>, List<string>> s_namesForExitsInRoomDescriptions = new Dictionary<List<AutomapDirection>, List<string>>()
        {
            { new List<AutomapDirection>() { AutomapDirection.North }, new List<string>(){ "north", "northward", "fore", "northern" } },
            { new List<AutomapDirection>() { AutomapDirection.South }, new List<string>(){ "south", "southward", "aft", "southern" } },
            { new List<AutomapDirection>() { AutomapDirection.East }, new List<string>(){ "east", "eastward", "starboard", "eastern" } },
            { new List<AutomapDirection>() { AutomapDirection.West }, new List<string>() { "west", "westward", "port", "western" } },
            { new List<AutomapDirection>() { AutomapDirection.NorthEast }, new List<string>() { "northeast", "northeastward", "northeastern" } },
            { new List<AutomapDirection>() { AutomapDirection.SouthEast }, new List<string>() { "southeast", "southeastward", "southeastern" } },
            { new List<AutomapDirection>() { AutomapDirection.SouthWest }, new List<string>() { "southwest", "southwestward", "southwestern" } },
            { new List<AutomapDirection>() { AutomapDirection.NorthWest }, new List<string>() { "northwest", "northwest", "northwestern" } },
            { new List<AutomapDirection>() { AutomapDirection.Up }, new List<string>() { "up", "upward", "upwards", "ascend", "above" } },
            { new List<AutomapDirection>() { AutomapDirection.Down }, new List<string>() { "down", "downward", "downwards", "descend", "below" } },
            { new List<AutomapDirection>() { AutomapDirection.North, AutomapDirection.South, AutomapDirection.East, AutomapDirection.West, AutomapDirection.NorthEast, AutomapDirection.NorthWest, AutomapDirection.SouthEast, AutomapDirection.SouthWest }, new List<string>() { "all directions", "every direction" } },
        };
    }

    enum AutomapDirection
    {
        North,
        South,
        East,
        West,
        NorthEast,
        SouthEast,
        SouthWest,
        NorthWest,
        Up,
        Down,
        In,
        Out
    };
}

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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trizbort
{
  internal enum AutomapDirection
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

  public sealed class Automap
  {
    private async Task WaitForStep()
    {
      // for diagnostic purposes, allow single stepping
      if (m_settings.SingleStep && !m_stepNow)
      {
        Status = "Automapping is waiting for you to step through it (with F11.)";
        while (!m_stepNow)
        {
          await Task.Delay(50);
        }
        m_stepNow = false;
      }
    }

    private async Task<string> WaitForNewLine(StreamReader reader, CancellationToken token)
    {
      if (reader.EndOfStream)
        Status = "Automapping is waiting for more text.";
      while (reader.EndOfStream)
      {
        await Task.Delay(500, token);
      }
      return await reader.ReadLineAsync();
    }

    private bool IsPrompt(string line, out string typedCommand)
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

    private bool ExtractRoomName(string line, string previousLine, out string name)
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
      } while (strippedSuffix);

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

    private bool IsRoomDescriptionWord(string word)
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

    private bool StartsWithCapitalOrNonLetter(string word)
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

    private bool IsAllCaps(string word)
    {
      return (word.ToUpper() == word);
    }

    private bool ExtractParagraph(List<string> lines, int lineIndex, out string paragraph)
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

    private Room FindRoom(string roomName, string roomDescription)
    {
      return m_canvas.FindRoom(roomName, roomDescription, delegate(string n, string d, Room r) { return Match(r, n, d); });
    }

    private bool? Match(Room room, string name, string description)
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

    private void NowInRoom(Room room)
    {
      m_lastKnownRoom = room;
      m_canvas.SelectRoom(room);
    }

    private void DeduceExitsFromDescription(Room room, string description)
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

    private async Task ProcessTranscriptText(List<string> lines)
    {
      string previousLine = null;
      for (var index = 0; index < lines.Count; ++index)
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
            // new room
            if (m_lastKnownRoom != null && m_lastMoveDirection != null)
            {
              // player moved to new room
              // if not added already, add room to map; and join it up to the previous one
              room = m_canvas.CreateRoom(m_lastKnownRoom, m_lastMoveDirection.Value, roomName);
              m_canvas.Connect(m_lastKnownRoom, m_lastMoveDirection.Value, room);
              Trace("{0}: {1} is now {2} from {3}.", FormatTranscriptLineForDisplay(line), roomName, m_lastMoveDirection.Value.ToString().ToLower(), m_lastKnownRoom.Name);
            }
            else 
            {
              if ((m_firstRoom) || (m_gameName == roomName))
              {
                // most likely this is the game title
                m_firstRoom = false;
                m_gameName = roomName;
                await WaitForStep();
              }
              else
              {
                // player teleported to new room;
                // don't connect it up, as we don't know how they got there
                room = m_canvas.CreateRoom(m_lastKnownRoom, roomName);
                Trace("{0}: teleported to new room, {1}.", FormatTranscriptLineForDisplay(line), roomName);
                await WaitForStep();
              }
            }
            if (room != null) {
              DeduceExitsFromDescription(room, roomDescription);
              NowInRoom(room);
            }
            await WaitForStep();
          }
          else if (room != m_lastKnownRoom)
          {
            // player moved to existing room
            if (m_lastKnownRoom != null && m_lastMoveDirection != null)
            {
              // player moved sensibly; ensure rooms are connected up
              m_canvas.Connect(m_lastKnownRoom, m_lastMoveDirection.Value, room);
              Trace("{0}: {1} is now {2} from {3}.", FormatTranscriptLineForDisplay(line), roomName, m_lastMoveDirection.Value.ToString().ToLower(), m_lastKnownRoom.Name);
            }

            NowInRoom(room);
            await WaitForStep();
          }
          else
          {
            // player didn't change rooms
            Trace("{0}: still in {1}.", FormatTranscriptLineForDisplay(line), m_lastKnownRoom.Name);
          }

          // add this description if the room doesn't have it already
          if (room != null) room.AddDescription(roomDescription);

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

    private string FormatTranscriptLineForDisplay(string line)
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

    private void ProcessPromptCommand(string command)
    {
      // unless we find one, this command does not involve moving in a given direction
      m_lastMoveDirection = null;

      // first process trizbort commands
      if (command.ToUpper().StartsWith(m_settings.AddRegionCommand.ToUpper()))
      {
        var regionName = command.Substring(m_settings.AddRegionCommand.Length).Trim();

        if (!string.IsNullOrEmpty(regionName) && m_lastKnownRoom != null)
        {
          // region already exists, just set the room to it
          if (Settings.Regions.Find(p => p.RegionName.Equals(regionName, StringComparison.OrdinalIgnoreCase)) == null)
          {
            Settings.Regions.Add(new Region {RegionName = regionName, TextColor = Settings.Color[Colors.LargeText], RColor = Settings.Color[Colors.Fill]});
          }
          m_lastKnownRoom.Region = regionName;
        }

        return;
      }

      if (command.ToUpper().StartsWith(m_settings.AddObjectCommand.ToUpper()))
      {
        // the user wants to add an object to the map
        var objectName = command.Substring(m_settings.AddObjectCommand.Length).Trim();

        if (!string.IsNullOrEmpty(objectName) && m_lastKnownRoom != null)
        {
          if (!string.IsNullOrEmpty(m_lastKnownRoom.Objects))
          {
            var alreadyExists = false;
            foreach (var line in m_lastKnownRoom.Objects.Replace("\r", string.Empty).Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries))
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

      // TODO: We entirely don't handle "go east. n. s then w." etc. and I don't see an easy way of doing so.

      // split the command into individual words
      var parts = command.Split(s_wordSeparators, StringSplitOptions.RemoveEmptyEntries);
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

    #region Private Member Variables

    private Room m_lastKnownRoom;
    private bool m_firstRoom = true;
    private string m_gameName = string.Empty;

    private AutomapDirection? m_lastMoveDirection;

    private IAutomapCanvas m_canvas;

    private AutomapSettings m_settings;
    private volatile bool m_stepNow;
    private string s_failureReason = string.Empty;

    private CancellationTokenSource m_tokenSource;

    private static readonly char[] s_wordSeparators = {' '};
    private static readonly string[] s_roomDecorativeSuffixMarkers = {",", "(", "[", "{", " - "};
    private static readonly string[] s_promptMarkers = {">"};
    private const int MaxCharactersBeforePrompt = 42;

    //static readonly string[] s_commandSeparators = { ".", " then " };
    private static readonly string[] s_wordsToStripFromCommands = {"the", "a", "to", "on"};
    private static readonly string[] s_wordsMeaningGo = {"go", "walk", "move"};

    private static readonly Dictionary<AutomapDirection, List<string>> s_namesForMovementCommands = new Dictionary<AutomapDirection, List<string>>
    {
      {AutomapDirection.North, new List<string> {"north", "n", "fore", "f"}},
      {AutomapDirection.South, new List<string> {"south", "s", "aft", "a"}},
      {AutomapDirection.East, new List<string> {"east", "e", "starboard", "sb"}},
      {AutomapDirection.West, new List<string> {"west", "w", "port", "p"}},
      {AutomapDirection.NorthEast, new List<string> {"northeast", "ne"}},
      {AutomapDirection.SouthEast, new List<string> {"southeast", "se"}},
      {AutomapDirection.SouthWest, new List<string> {"southwest", "sw"}},
      {AutomapDirection.NorthWest, new List<string> {"northwest", "nw"}},
      {AutomapDirection.Up, new List<string> {"up", "u"}},
      {AutomapDirection.Down, new List<string> {"down", "d"}},
      {AutomapDirection.In, new List<string> {"in", "inside"}},
      {AutomapDirection.Out, new List<string> {"out", "outside"}}
    };

    private static readonly Dictionary<List<AutomapDirection>, List<string>> s_namesForExitsInRoomDescriptions = new Dictionary<List<AutomapDirection>, List<string>>
    {
      {new List<AutomapDirection> {AutomapDirection.North}, new List<string> {"north", "northward", "fore", "northern"}},
      {new List<AutomapDirection> {AutomapDirection.South}, new List<string> {"south", "southward", "aft", "southern"}},
      {new List<AutomapDirection> {AutomapDirection.East}, new List<string> {"east", "eastward", "starboard", "eastern"}},
      {new List<AutomapDirection> {AutomapDirection.West}, new List<string> {"west", "westward", "port", "western"}},
      {new List<AutomapDirection> {AutomapDirection.NorthEast}, new List<string> {"northeast", "northeastward", "northeastern"}},
      {new List<AutomapDirection> {AutomapDirection.SouthEast}, new List<string> {"southeast", "southeastward", "southeastern"}},
      {new List<AutomapDirection> {AutomapDirection.SouthWest}, new List<string> {"southwest", "southwestward", "southwestern"}},
      {new List<AutomapDirection> {AutomapDirection.NorthWest}, new List<string> {"northwest", "northwest", "northwestern"}},
      {new List<AutomapDirection> {AutomapDirection.Up}, new List<string> {"up", "upward", "upwards", "ascend", "above"}},
      {new List<AutomapDirection> {AutomapDirection.Down}, new List<string> {"down", "downward", "downwards", "descend", "below"}},
      {new List<AutomapDirection> {AutomapDirection.North, AutomapDirection.South, AutomapDirection.East, AutomapDirection.West, AutomapDirection.NorthEast, AutomapDirection.NorthWest, AutomapDirection.SouthEast, AutomapDirection.SouthWest}, new List<string> {"all directions", "every direction"}}
    };

    #endregion

    #region Static Initialization

    // This implements the static initialization design pattern for a singleton.
    // It prevents having two automap instances trying to write to the map simultaneously.

    private Automap()
    {
      Status = "Automap is not running.";
    }

    public static Automap Instance { get; } = new Automap();

    #endregion

    #region Public Interface

    public void Step()
    {
      m_stepNow = true;
    }

    public void RunToCompletion()
    {
      m_settings.SingleStep = false;
      Step();
    }

    public string Status { get; private set; }

    public bool Running
    {
      get { return (m_tokenSource != null && !m_tokenSource.IsCancellationRequested); }
    }

    public void Stop()
    {
      if (m_tokenSource != null)
      {
        try
        {
          m_tokenSource.Cancel();
        }
        catch (ObjectDisposedException)
        {
          m_tokenSource = null;
        }
      }

      Status = "Automap is not running.";
    }

    internal async void Start(IAutomapCanvas canvas, AutomapSettings settings)
    {
      if (Running)
      {
        Stop();
      }

      m_canvas = canvas;
      m_settings = settings;
      m_firstRoom = true;
      Debug.Assert(m_settings.AssumeRoomsWithSameNameAreSameRoom || m_settings.VerboseTranscript, "Must assume rooms with same name are same room unless transcript is verbose.");
      Status = "Automapping has started.";

      try
      {
        using (var stream = File.Open(m_settings.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var reader = new StreamReader(stream))
        using (m_tokenSource = new CancellationTokenSource())
        {
          var lastline = "";

          if (m_settings.ContinueTranscript)
          {
            while (!reader.EndOfStream)
              lastline = await reader.ReadLineAsync();
          }

          // keep track of lines we read between here and the next prompt
          var linesBetweenPrompts = new List<string>();
          Status = "Automapping is processing the transcript.";

          var promptLine = string.Empty;
          var line = string.Empty;
          var atFileEnd = false;
          // loop until cancelled
          while (true)
          {
            if (m_settings.ContinueTranscript)
            {
              line = lastline;
              m_settings.ContinueTranscript = false;
            }
            else
            {
              // ...read a line of text
              try
              {
                line = await WaitForNewLine(reader, m_tokenSource.Token);
                atFileEnd = reader.EndOfStream; // store this now so that it's still valid when we use it below
              }
              catch (TaskCanceledException)
              {
                break;
              }
            }

            //Trace("[" + line + "]");
            string command;
            if (IsPrompt(line, out command))
            {
              // this is a prompt line

              // let's process everything leading up to it since the last prompt, but not necessarily this new prompt itself
              await ProcessTranscriptText(linesBetweenPrompts);

              // we've now dealt with all lines to this point
              linesBetweenPrompts.Clear();

              // handle the case where we're at the end of the file, waiting for user input
              if (atFileEnd)
              {
                try
                {
                  // we've already read the prompt, now just read the command when the player enters it
                  command = (await WaitForNewLine(reader, m_tokenSource.Token)).Trim();
                }
                catch (TaskCanceledException)
                {
                  break;
                }
              }

              // process the next command
              ProcessPromptCommand(command);


              Trace("{0}: {1}{2}", FormatTranscriptLineForDisplay(line), m_lastMoveDirection != null ? "GO " : string.Empty, m_lastMoveDirection != null ? m_lastMoveDirection.Value.ToString().ToUpperInvariant() : string.Empty);
            }
            else
            {
              // this line isn't a prompt;
              // hang onto it for now in case we meet a prompt shortly.
              linesBetweenPrompts.Add(line);
            }
          }
        }
      }
      catch (IOException ex)
      {
        // couldn't read from the file
        Trace("Automap: Error reading line in file.\nError message: " + ex.Message);
        MessageBox.Show("Error opening transcript file:\n" + ex.Message + "\n\nAutomapping halted.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (UnauthorizedAccessException)
      {
        MessageBox.Show("Could not gain access to the transcript file. Your interpreter may be restricting access to it. Try again in a few minutes " +
                        "or with scripting off in your interpreter.\n\nAutomapping halted.", "Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      Trace("Automap: Gentle thread exit.");
      Status = "Automapping has completed.";
    }

    #endregion

    #region Debugging

    [Conditional("DEBUG")]
    private void SetFailureReason(string format, params object[] args)
    {
      s_failureReason = string.Format(format, args);
    }

    [Conditional("DEBUG")]
    private void ClearFailureReason()
    {
      s_failureReason = string.Empty;
    }

    private string GetFailureReason()
    {
      return s_failureReason;
    }

    private bool HaveFailureReason()
    {
      return !string.IsNullOrEmpty(s_failureReason);
    }

    [Conditional("DEBUG")]
    private static void Trace(string format, params object[] args)
    {
      Debug.WriteLine(format, args);
    }

    #endregion
  }
}
using CommandLine;

namespace Trizbort.Domain.Application
{

  public class CommandLineOptions
  {
    [Value(0)]
    public string Executable { get; set; }

    [Value(1)]
    public string FileName { get; set; }

    [Option('a', "loadlastproject", HelpText = "Load the last opened project.")]
    public bool LoadLastProject { get; set; }

    [Option('m',"automap", HelpText = "Start automap with given transcript.")]
    public string Transcript { get; set; }

    [Option('q',"quicksave", HelpText="Quick save the map to the current Trizbort file.")]
    public string QuickSave { get; set; }

    [Option('s', "smartsave", HelpText = "SmartSave the loaded file")]
    public bool SmartSave { get; set; }

    [Option('n', "name", HelpText = "Name the current map.")]
    public string Name { get; set; }

    [Option('x', "exit", HelpText = "Exit Trizbort.")]
    public bool Exit { get; set; }

    [Option("inform6", HelpText = "Export to I6.")]
    public string I6 { get; set; }

    [Option("inform7", HelpText = "Export to I7.")]
    public string I7 { get; set; }

    [Option("tads", HelpText = "Export to Tads.")]
    public string Tads { get; set; }

    [Option("alan", HelpText = "Export to Alan.")]
    public string Alan { get; set; }

    [Option("hugo", HelpText = "Export to Hugo.")]
    public string Hugo { get; set; }

    [Option("zil", HelpText = "Export to Zil.")]
    public string Zil { get; set; }

    [Option("quest", HelpText = "Export to Quest.")]
    public string Quest { get; set; }

    [Option("quest rooms", HelpText = "Export to Quest section.")]
    public string QuestRooms { get; set; }
  }
}
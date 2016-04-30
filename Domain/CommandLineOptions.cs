using CommandLine;

namespace Trizbort.Domain
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
  }
}
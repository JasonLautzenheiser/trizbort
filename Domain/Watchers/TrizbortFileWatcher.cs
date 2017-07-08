using System;
using System.IO;
using System.Windows.Forms;

namespace Trizbort.Domain.Watchers
{
  public class TrizbortFileWatcher : IDisposable
  {
    public event EventHandler ReloadMap;
    private readonly FileSystemWatcher watcher = new FileSystemWatcher();

    public TrizbortFileWatcher()
    {
      watcher.NotifyFilter = NotifyFilters.LastWrite;
      watcher.Changed += Changed;
    }

    public void InitializeWatcher(string fileToWatch)
    {
      StopWatcher();
      watcher.Path = Path.GetDirectoryName(fileToWatch);
      watcher.Filter = Path.GetFileName(fileToWatch);
      StartWatcher();
    }

    public void StartWatcher()
    {
      watcher.EnableRaisingEvents = true;
    }

    public void StopWatcher()
    {
      watcher.EnableRaisingEvents = false;
    }

    private void Changed(object sender, FileSystemEventArgs e)
    {
      StopWatcher();
      Project.Current.Canvas.BeginInvoke(new Action(() =>
      {
        if (MessageBox.Show(Project.Current.Canvas, $"This map has been modified by another program.{Environment.NewLine}Do you want to reload it{dirtyMessage()}?", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          OnReloadMap();
        } else
        {
          StartWatcher();
        }
      }));
    }

    private string dirtyMessage()
    {
      if (!Project.Current.IsDirty) return "";
      return " and lose changes made in Trizbort";
    }

    public void Dispose()
    {
      watcher?.Dispose();
    }

    protected virtual void OnReloadMap()
    {
      ReloadMap?.Invoke(this, EventArgs.Empty);   
    }
  }
}
using System;
using System.IO;
using System.Windows.Forms;
using Trizbort.Domain.Application;

namespace Trizbort.Domain.Watchers {
  public class TrizbortFileWatcher : IDisposable {
    private readonly FileSystemWatcher watcher = new FileSystemWatcher();

    public TrizbortFileWatcher() {
      watcher.NotifyFilter = NotifyFilters.LastWrite;
      watcher.Changed += Changed;
    }

    public void Dispose() {
      watcher?.Dispose();
    }

    public void InitializeWatcher(string fileToWatch) {
      StopWatcher();
      watcher.Path = Path.GetDirectoryName(fileToWatch);
      watcher.Filter = Path.GetFileName(fileToWatch);
      StartWatcher();
    }

    public event EventHandler ReloadMap;

    public void StartWatcher() {
      watcher.EnableRaisingEvents = true;
    }

    public void StopWatcher() {
      watcher.EnableRaisingEvents = false;
    }

    protected virtual void OnReloadMap() {
      ReloadMap?.Invoke(this, EventArgs.Empty);
    }

    private void Changed(object sender, FileSystemEventArgs e) {
      StopWatcher();
      Project.Current.Canvas.BeginInvoke(new Action(() => {
        if (MessageBox.Show(Project.Current.Canvas, $"This map has been modified by another program.{Environment.NewLine}Do you want to reload it{dirtyMessage()}?", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          OnReloadMap();
        else
          StartWatcher();
      }));
    }

    private string dirtyMessage() {
      if (!Project.Current.IsDirty) return "";
      return " and lose changes made in Trizbort";
    }
  }
}
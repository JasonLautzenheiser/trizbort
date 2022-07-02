using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Trizbort.Domain.StatusBar; 

public enum StatusItems {
  tsb_Info = 0,
  tsb_CapsLock,
  tsb_NumLock,
  tsb_Zoom
}

public class StatusItem {
  public StatusItems Id { get;set; }
  public bool Show { get; set; }
  public ToolStripStatusLabel Control { get; set; }
  public IStatusWidget Widget { get; set; }
}



public class Status {
  public Status(StatusStrip statusBar) {
    this.statusBar = statusBar;
    this.statusBar.MouseLeave += showDefaultInfoMessage;
  }

  private void showDefaultInfoMessage(object sender, EventArgs e) {
    updateInfoMessage(string.Empty);
  }

  private StatusStrip statusBar { get; set; }
  public List<StatusItem> Items { get; set; } = null;
  public string LastStatus { get; set; }

  public void UpdateStatusBar() {
    if (Items == null) {
      setDefaultItems();
      addItemsToStatusBar();
    }

    foreach (var statusItem in Items.Where(p=>p.Id != StatusItems.tsb_Info)) {
      statusItem.Control.Text =  statusItem.Widget.DisplayText();
      statusItem.Control.ForeColor = statusItem.Widget.DisplayColor;
    }
  }

  private void addItemsToStatusBar() {
    foreach (var statusItem in Items) {
      if (statusItem.Id == StatusItems.tsb_Info) {
        var infoLabel = new ToolStripStatusLabel(statusItem.Id.ToString()) {
          Spring = true,
          Alignment = ToolStripItemAlignment.Left,
          TextAlign = ContentAlignment.MiddleLeft
        };
        statusBar.Items.Add(infoLabel);
        statusItem.Control = infoLabel;
      } else {
        var itemLabel = new ToolStripStatusLabel(statusItem.Id.ToString()) {Tag = statusItem.Id};
        itemLabel.MouseEnter += showHelp;
        itemLabel.Click += handleClick;
        statusBar.Items.Add(itemLabel);
        statusItem.Control = itemLabel;
      }
    }
      
  }

  private void handleClick(object sender, EventArgs e) {
    var control = (ToolStripStatusLabel) sender;
    var widget = Items.Find(p => p.Id == (StatusItems) control.Tag);
    widget.Widget.ClickHandler();
  }

  private void showHelp(object sender, EventArgs eventArgs) {
    var control = (ToolStripStatusLabel) sender;

    var helpItem = Items.Find(p => p.Id == (StatusItems) control.Tag);
      
    updateInfoMessage(helpItem.Widget.HelpText);
  }

  private void updateInfoMessage(string text) {
    var item = Items.Find(p => p.Id == StatusItems.tsb_Info);
    item.Control.Text = text;
  }

  private void updateInfoMessage(IStatusWidget helpItem) {
    updateInfoMessage(helpItem.HelpText);
  }

  private void setDefaultItems() {
    Items = new List<StatusItem> {
      new StatusItem {Id = StatusItems.tsb_Info, Show = true},
      new StatusItem {Id = StatusItems.tsb_CapsLock, Show = true, Widget = new CapsLockStatusWidget()},
      new StatusItem {Id = StatusItems.tsb_NumLock, Show = true, Widget = new NumLockStatusWidget()}, 
      new StatusItem {Id = StatusItems.tsb_Zoom, Show = true, Widget = new ZoomStatusWidget()}
    };
  }
}
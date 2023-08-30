using System;
using System.Drawing;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.UI.Controls;
using Trizbort.Util;

namespace Trizbort.Domain.StatusBar {
  public class ZoomStatusWidget : IStatusWidget {
    // TODO ContextMenu is no longer supported. Use ContextMenuStrip instead. For more details see https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls
    ContextMenuStrip menu;

    public StatusItems Id => StatusItems.tsb_CapsLock;
    public string Name => "Zoom";
    public string MenuName => "Map Zoom";
    public string HelpText => "Mousewheel Up/Down to change zoom, Ctrl Mousewheel gives finer control.";

    public Color DisplayColor => Color.Black;

    public string DisplayText() {
      return Project.Current.Canvas.ZoomFactor.ToString("P");
    }

    public void ClickHandler() {
      menu.Show(Project.Current.Canvas, Cursor.Position);
    }

    public ZoomStatusWidget() {
      // TODO ContextMenu is no longer supported. Use ContextMenuStrip instead. For more details see https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls
      menu = new ContextMenuStrip();
      menu.Items.Add("Zoom 300%", null, (o, args) => setZoom(o, 3.00f));
      menu.Items.Add("Zoom 250%", null,(o, args) => setZoom(o, 2.50f));
      menu.Items.Add("Zoom 200%", null,(o, args) => setZoom(o, 2.00f));
      menu.Items.Add("Zoom 175%", null,(o, args) => setZoom(o, 1.75f));
      menu.Items.Add("Zoom 150%",null, (o, args) => setZoom(o, 1.50f));
      menu.Items.Add("Zoom 125%", null,(o, args) => setZoom(o, 1.25f));
      menu.Items.Add("Zoom 100%", null,(o, args) => setZoom(o, 1.00f));
      menu.Items.Add("Zoom 75%", null,(o, args) => setZoom(o, 0.75f));
      menu.Items.Add("Zoom 50%", null,(o, args) => setZoom(o, 0.50f));
    }

    private void setZoom(object sender, float zoomFactor) {
      Project.Current.Canvas.ZoomFactor = zoomFactor;
    }
  }
}
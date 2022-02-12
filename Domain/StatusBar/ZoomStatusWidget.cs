using System;
using System.Drawing;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.UI.Controls;
using Trizbort.Util;

namespace Trizbort.Domain.StatusBar {
  public class ZoomStatusWidget : IStatusWidget {
    ContextMenu menu;

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
      menu = new ContextMenu();
      menu.MenuItems.Add("Zoom 300%", (o, args) => setZoom(o, 3.00f));
      menu.MenuItems.Add("Zoom 250%", (o, args) => setZoom(o, 2.50f));
      menu.MenuItems.Add("Zoom 200%", (o, args) => setZoom(o, 2.00f));
      menu.MenuItems.Add("Zoom 175%", (o, args) => setZoom(o, 1.75f));
      menu.MenuItems.Add("Zoom 150%", (o, args) => setZoom(o, 1.50f));
      menu.MenuItems.Add("Zoom 125%", (o, args) => setZoom(o, 1.25f));
      menu.MenuItems.Add("Zoom 100%", (o, args) => setZoom(o, 1.00f));
      menu.MenuItems.Add("Zoom 75%", (o, args) => setZoom(o, 0.75f));
      menu.MenuItems.Add("Zoom 50%", (o, args) => setZoom(o, 0.50f));
    }

    private void setZoom(object sender, float zoomFactor) {
      Project.Current.Canvas.ZoomFactor = zoomFactor;
    }
  }
}
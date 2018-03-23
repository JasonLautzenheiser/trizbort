using System.Collections.Generic;
using System.Linq;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;
using Trizbort.UI.Controls;

namespace Trizbort.Domain.Controllers {
  public class CanvasController {
    private readonly Canvas canvas;


    public CanvasController() {
      canvas = TrizbortApplication.MainForm.Canvas;
    }

    public void EnsureVisible(Element element) {
      if (element == null) return;
      var rect = Rect.Empty;
      rect = element.UnionBoundsWith(rect, false);
      if (rect != Rect.Empty) canvas.Origin = rect.Center;
    }

    public void SelectElements(List<Element> elements) {
      canvas.SelectElements(elements);
    }

    public void SelectRoomClosestToCenterOfViewport() {
      // select the room closest to the center of the viewport
      var viewportCenter = canvas.Viewport.Center;
      Room closestRoom = null;
      var closestDistance = float.MaxValue;
      foreach (var element in Project.Current.Elements.OfType<Room>()) {
        var roomCenter = element.InnerBounds.Center;
        var distance = roomCenter.Distance(viewportCenter);

        if (!(distance < closestDistance)) continue;
        closestRoom = element;
        closestDistance = distance;
      }

      canvas.SelectedElement = closestRoom;
      EnsureVisible(canvas.SelectedElement);
    }

    public void SelectStartRoom() {
      var startRoom = Project.Current.Elements.OfType<Room>().FirstOrDefault(p => p.IsStartRoom);
      if (startRoom != null) {
        canvas.SelectedElement = startRoom;
        EnsureVisible(startRoom);
      }
    }

    public void SetConnectionFlow(ConnectionFlow connectionFlow) {
      var elements = canvas.SelectedConnections;
      setConnectionFlow(elements, connectionFlow);
    }

    public void SetConnectionLabel(ConnectionLabel label) {
      var elements = canvas.SelectedConnections;
      setConnectionLabel(elements, label);
    }

    public void SetConnectionStyle(ConnectionStyle style) {
      var elements = canvas.SelectedConnections;
      setConnectionStyle(elements, style);
    }

    private void setConnectionFlow(List<Connection> connections, ConnectionFlow connectionFlow) {
      connections.ForEach(p => p.Flow = connectionFlow);
      canvas.NewConnectionFlow = connectionFlow;
    }

    private void setConnectionLabel(List<Connection> connections, ConnectionLabel label) {
      connections.ForEach(p => p.SetText(label));
      canvas.NewConnectionLabel = label;
    }

    private void setConnectionStyle(List<Connection> connections, ConnectionStyle style) {
      connections.ForEach(p => p.Style = style);
      canvas.NewConnectionStyle = style;
    }
  }
}
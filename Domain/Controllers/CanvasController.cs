using System.Collections.Generic;
using System.Linq;
using Trizbort.Domain.Commands;

namespace Trizbort.Domain.Controllers
{
  public class CanvasController
  {
    private readonly UI.Controls.Canvas canvas;

    public CanvasController(UI.Controls.Canvas canvas)
    {
      this.canvas = canvas;
    }

    public void SelectRoomClosestToCenterOfViewport()
    {
      // select the room closest to the center of the viewport
      var viewportCenter = canvas.Viewport.Center;
      Room closestRoom = null;
      var closestDistance = float.MaxValue;
      foreach (var element in Project.Current.Elements.OfType<Room>())
      {
        var roomCenter = element.InnerBounds.Center;
        var distance = roomCenter.Distance(viewportCenter);

        if (!(distance < closestDistance)) continue;
        closestRoom = element;
        closestDistance = distance;
      }

      canvas.SelectedElement = closestRoom;
      EnsureVisible(canvas.SelectedElement);
    }

    public void SelectStartRoom()
    {
      var startRoom = Project.Current.Elements.OfType<Room>().FirstOrDefault(p => p.IsStartRoom);
      if (startRoom != null)
      {
        canvas.SelectedElement = startRoom;
        EnsureVisible(startRoom);
      }

    }

    public void EnsureVisible(Element element)
    {
      if (element == null) return;
      var rect = Rect.Empty;
      rect = element.UnionBoundsWith(rect, false);
      if (rect != Rect.Empty)
      {
        canvas.Origin = rect.Center;
      }
    }

    public void SetConnectionFlow(ConnectionFlow connectionFlow)
    {
      var elements = canvas.SelectedConnections;
      setConnectionFlow(elements, connectionFlow);
    }

    private void setConnectionFlow(List<Connection> connections, ConnectionFlow connectionFlow)
    {
      connections.ForEach(p=>p.Flow = connectionFlow);
      canvas.NewConnectionFlow = connectionFlow;
    }

    public void SetConnectionStyle(ConnectionStyle style)
    {
      var elements = canvas.SelectedConnections;
      setConnectionStyle(elements, style);
    }

    private void setConnectionStyle(List<Connection> connections, ConnectionStyle style)
    {
      connections.ForEach(p=>p.Style = style);
      canvas.NewConnectionStyle = style;
    }

    public void SetConnectionLabel(ConnectionLabel label)
    {
      var elements = canvas.SelectedConnections;
      setConnectionLabel(elements,label);
    }

    private void setConnectionLabel(List<Connection> connections, ConnectionLabel label)
    {
      connections.ForEach(p => p.SetText(label));
      canvas.NewConnectionLabel = label;
    }

  }
}
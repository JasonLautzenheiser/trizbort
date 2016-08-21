using System;
using System.Linq;
using Trizbort.Domain.Commands;
using Trizbort.Domain.Enums;

namespace Trizbort.Domain.Controllers
{
  public class CommandController
  {
    private readonly UI.Controls.Canvas canvas;

    public CommandController(UI.Controls.Canvas canvas)
    {
      this.canvas = canvas;
    }

    public void SelectRegions()
    {
      var regions = canvas.SelectedRooms.Select(p => p.Region).Distinct().ToList();
      var cmd = new SelectCommand();
      cmd.Execute(canvas, SelectTypes.Region, regions);
    }

    public void Select(SelectTypes type)
    {
      var cmd = new SelectCommand();
      cmd.Execute(canvas, type);
    }

    public void SelectStartRoom()
    {
      var controller = new CanvasController();
      controller.SelectStartRoom();
    }

    public void SelectRoomClosestToCenterOfViewport()
    {
      var controller = new CanvasController();
      controller.SelectRoomClosestToCenterOfViewport();
    }

    public void SetRoomShape(RoomShape shape)
    {
      RoomController controller = new RoomController();
      controller.SetRoomShape(canvas.SelectedRooms,shape);
    }

    public void SetRoomLighting(LightingActionType type)
    {
      RoomController controller = new RoomController();
      switch (type)
      {
        case LightingActionType.Toggle:
          controller.ToggleDarkness(canvas.SelectedRooms);
          break;
        case LightingActionType.ForceLight:
          controller.ForceLighted(canvas.SelectedRooms);
          break;
        case LightingActionType.ForceDark:
          controller.ForceDarkness(canvas.SelectedRooms);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }

    public void ShowElementProperties(Element element)
    {
      var controller = new ElementController();
      controller.ShowElementProperties(element);
    }


    public void SetConnectionFlow(ConnectionFlow flow)
    {
      var controller = new CanvasController();

      controller.SetConnectionFlow(flow);
    }

    public void ToggleConnectionFlow(ConnectionFlow flow)
    {
      var f = flow == ConnectionFlow.OneWay ? ConnectionFlow.TwoWay : ConnectionFlow.OneWay;
      var controller = new CanvasController();

      controller.SetConnectionFlow(f);
    }

    public void SetConnectionStyle(ConnectionStyle style)
    {
      var controller = new CanvasController();

      controller.SetConnectionStyle(style);
    }

    public void ToggleConnectionStyle(ConnectionStyle style)
    {
      var f = style == ConnectionStyle.Dashed ? ConnectionStyle.Solid : ConnectionStyle.Dashed;
      var controller = new CanvasController();

      controller.SetConnectionStyle(f);
    }

    public void SetConnectionLabel(ConnectionLabel label)
    {
      var controller = new CanvasController();

      controller.SetConnectionLabel(label);
    }

    public void MakeVisible(Element element)
    {
      var controller = new CanvasController();
      controller.EnsureVisible(element);
    }
  }
}
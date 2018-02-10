﻿using System;
using System.Linq;
using Trizbort.Domain.Application;
using Trizbort.Domain.Commands;
using Trizbort.Domain.Elements;
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

    public void SetEndRoom()
    {
      foreach (var selectedRoom in canvas.SelectedRooms)
      {
        selectedRoom.IsEndRoom = !selectedRoom.IsEndRoom;
      }
    }

    public void SetStartRoom()
    {
      if (canvas.SelectedRooms.Count == 1)
      {
        if (canvas.SelectedRooms.First().IsStartRoom)
        {
          canvas.SelectedRooms.First().IsStartRoom = false;
        }
        else
        {
          Project.Current.Elements.OfType<Room>().Where(p => p.IsStartRoom).ToList().ForEach(p => p.IsStartRoom = false);
          canvas.SelectedRooms.First().IsStartRoom = true;
        }
      }
    }

    public void SetValidation(ValidationType validationType)
    {
      switch (validationType) {
        case ValidationType.RoomUniqueName:
          Project.Current.MustHaveUniqueNames = !Project.Current.MustHaveUniqueNames;
          break;
        case ValidationType.RoomDescription:
          Project.Current.MustHaveDescription = !Project.Current.MustHaveDescription;
          break;
        case ValidationType.RoomSubTitle:
          Project.Current.MustHaveSubtitle = !Project.Current.MustHaveSubtitle;
          break;
        case ValidationType.RoomDanglingConnection:
          Project.Current.MustHaveNoDanglingConnectors = !Project.Current.MustHaveNoDanglingConnectors;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(validationType), validationType, null);
      }
    }

    public void BringToFront()
    {
      int newIndex = canvas.GetHighestZOrderIndex();
      foreach (var element in canvas.SelectedElements)
      {
        element.ZOrder = newIndex;
      }
    }

    public void SendToBack()
    {
      int newIndex = canvas.GetLowestZOrderIndex();
      foreach (var element in canvas.SelectedElements)
      {
        element.ZOrder = newIndex;
      }
    }
  }
}
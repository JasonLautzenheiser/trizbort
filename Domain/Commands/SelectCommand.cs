using System;
using System.Collections.Generic;
using Trizbort.Domain.Enums;
using Trizbort.UI.Controls;

namespace Trizbort.Domain.Commands; 

public sealed class SelectCommand : ICanvasCommand<SelectTypes> {
  public void Execute(Canvas canvas, SelectTypes value) {
    switch (value) {
      case SelectTypes.None:
        canvas.SelectedElement = null;
        break;
      case SelectTypes.All:
        canvas.SelectAll();
        break;
      case SelectTypes.Rooms:
        canvas.SelectAllRooms();
        break;
      case SelectTypes.Connections:
        canvas.SelectAllConnections();
        break;
      case SelectTypes.UnconnectedRooms:
        canvas.SelectAllUnconnectedRooms();
        break;
      case SelectTypes.DanglingConnections:
        canvas.SelectDanglingConnections();
        break;
      case SelectTypes.SelfLoopingConnections:
        canvas.SelectSelfLoopingConnections();
        break;
      case SelectTypes.RoomsWithObjects:
        canvas.SelectRoomsWithObjects();
        break;
      case SelectTypes.RoomsWithOutObjects:
        canvas.SelectRoomsWithoutObjects();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(value), value, null);
    }
  }


  public void Execute(Canvas canvas, SelectTypes value, object other) {
    switch (value) {
      case SelectTypes.Region:
        if (other is IEnumerable<string>)
          canvas.SelectAllRegion((IEnumerable<string>) other);
        break;
    }
  }
}
using System.Collections.Generic;
using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Controllers; 

public sealed class RoomController {
  public void ForceDarkness(List<Room> selectedRooms) {
    selectedRooms.ForEach(room => room.IsDark = true);
  }

  public void ForceLighted(List<Room> selectedRooms) {
    selectedRooms.ForEach(room => room.IsDark = false);
  }

  public void SetRoomShape(List<Room> selectedRooms, RoomShape shape) {
    selectedRooms.ForEach(room => room.Shape = shape);
  }

  public void ToggleDarkness(List<Room> selectedRooms) {
    selectedRooms.ForEach(room => room.IsDark = !room.IsDark);
  }
}
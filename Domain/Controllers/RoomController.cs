using System.Collections.Generic;

namespace Trizbort.Domain.Controllers
{
  public class RoomController
  {
    public void ToggleDarkness(List<Room> selectedRooms)
    {
      selectedRooms.ForEach(room => room.IsDark = !room.IsDark);
    }

    public void ForceDarkness(List<Room> selectedRooms)
    {
      selectedRooms.ForEach(room => room.IsDark = true);
    }

    public void ForceLighted(List<Room> selectedRooms)
    {
      selectedRooms.ForEach(room => room.IsDark = false);
    }

    public void SetRoomShape(List<Room> selectedRooms, RoomShape shape)
    {
      selectedRooms.ForEach(room => room.Shape = shape);
    }
  }
}
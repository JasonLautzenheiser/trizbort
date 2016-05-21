using System.Collections.Generic;

namespace Trizbort.Domain
{
  public static class CanvasController
  {
    public static void ToggleDarkness(List<Room> selectedRooms)
    {
      selectedRooms.ForEach(room => room.IsDark = !room.IsDark);
    }

    public static void ForceDarkness(List<Room> selectedRooms)
    {
      selectedRooms.ForEach(room => room.IsDark = true);
    }

    public static void ForceLighted(List<Room> selectedRooms)
    {
      selectedRooms.ForEach(room => room.IsDark = false);
    }
  }
}
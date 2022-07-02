namespace Trizbort.Domain.Enums; 

public enum SelectTypes {
  None,
  All,
  Rooms,
  Connections,
  UnconnectedRooms,
  DanglingConnections,
  SelfLoopingConnections,
  RoomsWithObjects,
  RoomsWithOutObjects,
  Region
}
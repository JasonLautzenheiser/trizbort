using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Trizbort.Domain.Controllers
{
  public class CopyController
  {
    public enum CopyType
    {
      Rooms,
      Colors
    }

    public enum VertexType
    {
      Dock,
      Point
    }

    public void CopyColors(Room selectedElement)
    {
      var colorProperties = selectedElement.GetType().GetProperties().Where(p => p.PropertyType == typeof(Color));
      var list = new List<CopyColorObj>();
      foreach (var prop in colorProperties)
      {
        var xx = new CopyColorObj
        {
          Name = prop.Name,
          Color = (Color) prop.GetValue(selectedElement)
        };

        list.Add(xx);
      }

      var obj = new CopyColorsObj
      {
        Colors = list,
        SecondFillLocation = selectedElement.SecondFillLocation
      };

      var clipboardText = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
      Clipboard.SetText(clipboardText);
    }

    public void CopyElements(List<Element> mSelectedElements)
    {
      var xx = new CopyObject { Rooms = new List<CopyRoomObj>(), Connections = new List<CopyConnectionObj>()};

      foreach (var element in mSelectedElements)
        if (element is Room)
        {
          var copy = createCopyObj(element as Room);
          xx.Rooms.Add(copy);
        }
        else if (element is Connection)
        {
          var copy = createCopyObj(element as Connection);
          xx.Connections.Add(copy);
        }

      var clipboardText = JsonConvert.SerializeObject(xx, Formatting.Indented, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
      Clipboard.SetText(clipboardText);
    }

    public ICopyObj PasteElements()
    {
      var clipboardText = Clipboard.GetText();

      ICopyObj xx;
      if (clipboardText.Contains("\"CopyType\": 1"))
      {
        xx = JsonConvert.DeserializeObject<CopyColorsObj>(clipboardText);
      }
      else
      {
        xx = JsonConvert.DeserializeObject<CopyObject>(clipboardText);
      }

      return xx;
    }

    public void SetConnection(Connection newConnection, CopyConnectionObj connection)
    {
      newConnection.ConnectionColor = connection.ConnectionColor;
      newConnection.Description = connection.Description;
      newConnection.Door = connection.Door;
      newConnection.EndText = connection.EndText;
      newConnection.Flow = connection.Flow;
      newConnection.MidText = connection.MidText;
      newConnection.Name = connection.Name;
      newConnection.StartText = connection.StartText;
      newConnection.Style = connection.Style;
    }

    public void SetRoom(Room newRoom, CopyRoomObj room)
    {
      newRoom.OldID = room.ID;
      newRoom.AddDescription(room.PrimaryDescription);
      newRoom.Shape = room.Shape;
      newRoom.Name = room.Name;
      newRoom.SubTitle = room.SubTitle;
      newRoom.IsDark = room.IsDark;
      newRoom.Objects = room.Objects;
      newRoom.ObjectsPosition = room.ObjectsPosition;
      newRoom.BorderStyle = room.BorderStyle;
      newRoom.Region = room.Region;
      newRoom.Corners = room.Corners;
      newRoom.RoundedCorners = room.RoundedCorners;
      newRoom.Octagonal = room.Octagonal;
      newRoom.Ellipse = room.Ellipse;
      newRoom.StraightEdges = room.StraightEdges;
      newRoom.AllCornersEqual = room.AllCornersEqual;
      newRoom.IsStartRoom = room.IsStartRoom;
      newRoom.IsEndRoom = room.IsEndRoom;
      newRoom.ArbitraryAutomappedPosition = room.ArbitraryAutomappedPosition;
      newRoom.RoomFill = room.RoomFill;
      newRoom.SecondFill = room.SecondFill;
      newRoom.SecondFillLocation = room.SecondFillLocation;
      newRoom.RoomBorder = room.RoomBorder;
      newRoom.RoomLargeText = room.RoomLargeText;
      newRoom.RoomSmallText = room.RoomSmallText;
      newRoom.OldID = room.OldID;
      newRoom.ObjectsCustomPositionDown = room.ObjectsCustomPositionDown;
      newRoom.ObjectsCustomPositionRight = room.ObjectsCustomPositionRight;
      newRoom.ObjectsCustomPosition = room.ObjectsCustomPosition;
      newRoom.Size = room.Size;
    }

    private CopyRoomObj createCopyObj(Room room)
    {
      var xx = new CopyRoomObj
      {
//        ID = room.ID,
        Name = room.Name,
        Shape = room.Shape,
        SubTitle = room.SubTitle,
        IsDark = room.IsDark,
        Objects = room.Objects,
        ObjectsPosition = room.ObjectsPosition,
        BorderStyle = room.BorderStyle,
        Region = room.Region,
        Corners = room.Corners,
        RoundedCorners = room.RoundedCorners,
        Octagonal = room.Octagonal,
        Ellipse = room.Ellipse,
        StraightEdges = room.StraightEdges,
        AllCornersEqual = room.AllCornersEqual,
        IsStartRoom = room.IsStartRoom,
        IsEndRoom = room.IsEndRoom,
        ArbitraryAutomappedPosition = room.ArbitraryAutomappedPosition,
        PrimaryDescription = room.PrimaryDescription,
        RoomFill = room.RoomFill,
        SecondFill = room.SecondFill,
        SecondFillLocation = room.SecondFillLocation,
        RoomBorder = room.RoomBorder,
        RoomLargeText = room.RoomLargeText,
        RoomSmallText = room.RoomSmallText,
        OldID = room.ID,
        ObjectsCustomPositionDown = room.ObjectsCustomPositionDown,
        ObjectsCustomPositionRight = room.ObjectsCustomPositionRight,
        ObjectsCustomPosition = room.ObjectsCustomPosition,
        Position = room.Position,
        Size = room.Size
      };


      return xx;
    }

    private CopyConnectionObj createCopyObj(Connection conn)
    {
      var xx = new CopyConnectionObj
      {
        ConnectionColor = conn.ConnectionColor,
        Description = conn.Description,
        Door = conn.Door,
        EndText = conn.EndText,
        Flow = conn.Flow,
        MidText = conn.MidText,
        Name = conn.Name,
        StartText = conn.StartText,
        Style = conn.Style,
        VertextList = new List<CopyVertexObj>()
      };

      var ii = 0;
      foreach (var vertex in conn.VertexList)
      {
        var yy = new CopyVertexObj {Index = ii};
        if (vertex.Port != null)
        {
          yy.Type = VertexType.Dock;
          yy.OwnerId = vertex.Port.Owner.ID;
          yy.PortId = vertex.Port.ID;
        }
        else
        {
          yy.Type = VertexType.Point;
          yy.Position = vertex.Position;
        }
        xx.VertextList.Add(yy);

        ii++;
      }

      return xx;
    }

    public class CopyObject : ICopyObj
    {
      public List<CopyConnectionObj> Connections;
      public List<CopyRoomObj> Rooms;
      public CopyType CopyType => CopyType.Rooms;
    }

    public class CopyConnectionObj
    {
      public Color ConnectionColor { get; set; }
      public string Description { get; set; }
      public Door Door { get; set; }
      public string EndText { get; set; }
      public ConnectionFlow Flow { get; set; }
      public string MidText { get; set; }
      public string Name { get; set; }
      public string StartText { get; set; }
      public ConnectionStyle Style { get; set; }
      public List<CopyVertexObj> VertextList { get; set; }
    }

    public class CopyVertexObj
    {
      public int Index { get; set; }
      public int OwnerId { get; set; }
      public string PortId { get; set; }
      public Vector Position { get; set; }
      public VertexType Type { get; set; }
    }

    public class CopyRoomObj
    {
      public bool AllCornersEqual { get; set; }
      public bool ArbitraryAutomappedPosition { get; set; }
      public BorderDashStyle BorderStyle { get; set; }
      public CornerRadii Corners { get; set; }
      public bool Ellipse { get; set; }
      public int ID { get; set; }
      public bool IsDark { get; set; }
      public bool IsEndRoom { get; set; }
      public bool IsStartRoom { get; set; }
      public string Name { get; set; }
      public string Objects { get; set; }
      public bool ObjectsCustomPosition { get; set; }
      public int ObjectsCustomPositionDown { get; set; }
      public int ObjectsCustomPositionRight { get; set; }
      public CompassPoint ObjectsPosition { get; set; }
      public bool Octagonal { get; set; }
      public int OldID { get; set; }
      public Vector Position { get; set; }
      public string PrimaryDescription { get; set; }
      public string Region { get; set; }
      public Color RoomBorder { get; set; }
      public Color RoomFill { get; set; }
      public Color RoomLargeText { get; set; }
      public Color RoomSmallText { get; set; }
      public bool RoundedCorners { get; set; }
      public Color SecondFill { get; set; }
      public string SecondFillLocation { get; set; }
      public RoomShape Shape { get; set; }
      public Vector Size { get; set; }
      public bool StraightEdges { get; set; }
      public string SubTitle { get; set; }
    }


    public class CopyColorsObj : ICopyObj
    {
      public List<CopyColorObj> Colors { get; set; }
      public CopyType CopyType => CopyType.Colors;
      public string SecondFillLocation { get; set; }
    }

    public class CopyColorObj
    {
      public Color Color { get; set; }
      public string Name { get; set; }
    }

    public interface ICopyObj
    {
      
    }

  }
}
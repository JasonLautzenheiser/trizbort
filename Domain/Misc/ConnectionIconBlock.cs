using System.Drawing;
using PdfSharp.Drawing;

namespace Trizbort.Domain.Misc
{
  public class ConnectionIconBlock
  {
    private const int HORIZONTAL_LINE_OFFSET = 15;
    private const int VERTICAL_STEM_OFFSET = 15;
    private const int VERTICAL_LINE_OFFSET = 5;
    private const int HORIZONTAL_STEM_OFFSET = 5;
    
    public ConnectionIconBlock(LineSegment segment, int offset)
    {
      Segment = segment;
      Offset = offset;
    }

    public LineSegment Segment { get; set; }
    public Bitmap Image { get; set; }
    //public Vector Position => CalculateBlockPosition();
    public int Offset { get; set; }

    public void DrawBlock(XGraphics graphics)
    {
      var bounds = new Rect(Segment.Start, Vector.Zero);

      var compassPoint = CompassPointHelper.GetCompassPointFromDirectionVector(Segment.Delta);
      var pos = bounds.GetCorner(compassPoint);
      Size offsets = new Size(0,0);

      switch (compassPoint)
      {
        case CompassPoint.NorthWest:
          offsets.Height = -(Offset * 12);
          offsets.Width = -(Offset * 10) - HORIZONTAL_LINE_OFFSET;
          break;

        case CompassPoint.NorthEast:
          offsets.Height = -(Offset * 12);
          offsets.Width = (Offset * 10) + HORIZONTAL_STEM_OFFSET;
          break;

        case CompassPoint.SouthEast:
          offsets.Height = (Offset * 12);
          offsets.Width = (Offset * 10) + HORIZONTAL_LINE_OFFSET;
          break;

        case CompassPoint.SouthWest:
          offsets.Height = (Offset*12) + VERTICAL_LINE_OFFSET;
          offsets.Width = -(Offset * 10) ;
          break;

        case CompassPoint.East:
          offsets.Height = VERTICAL_LINE_OFFSET;
          offsets.Width = (Offset*16) + HORIZONTAL_STEM_OFFSET;
          break;

        case CompassPoint.West:
          offsets.Height = VERTICAL_LINE_OFFSET;
          offsets.Width = -(Offset * 16) - (HORIZONTAL_LINE_OFFSET);
          break;

        case CompassPoint.South:
          offsets.Height = (Offset * 16) + VERTICAL_STEM_OFFSET;
          offsets.Width = -HORIZONTAL_LINE_OFFSET;
          break;

        case CompassPoint.North:
          offsets.Height = -(Offset * 16) - VERTICAL_STEM_OFFSET;
          offsets.Width = -HORIZONTAL_LINE_OFFSET;
          break;

      }
      
      //      int dist = 15;
//      if (compassPoint == CompassPoint.NorthWest)
//        bounds.Inflate(-dist, -dist + 9);
//
//      if (compassPoint == CompassPoint.NorthEast)
//        bounds.Inflate(-dist - 5, -dist);
//
//      if (compassPoint == CompassPoint.SouthEast)
//        bounds.Inflate(-dist, dist - 9);
//
//      if (compassPoint == CompassPoint.SouthWest)
//        bounds.Inflate(-dist, -dist);
//
      

      graphics.DrawImage(Image, pos.ToPointF() + offsets);

    }
  }
}
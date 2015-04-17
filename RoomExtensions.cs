using System;
using System.Drawing.Drawing2D;

namespace Trizbort
{
  public static class RoomExtensions
  {
    public static DashStyle ConvertToDashStyle(this BorderDashStyle cxx)
    {
      switch (cxx)
      {
        case BorderDashStyle.Solid:
          return DashStyle.Solid;
          break;
        case BorderDashStyle.Dot:
          return DashStyle.Dot;
          break;
        case BorderDashStyle.Dash:
          return DashStyle.Dash;
          break;
        case BorderDashStyle.DashDot:
          return DashStyle.DashDot;
          break;
        case BorderDashStyle.DashDotDot:
          return DashStyle.DashDotDot;
          break;
        case BorderDashStyle.Custom:
        case BorderDashStyle.None:
          return DashStyle.Custom;
        break;
        default:
          throw new ArgumentOutOfRangeException("cxx", cxx, null);
      }
    }
  }
}
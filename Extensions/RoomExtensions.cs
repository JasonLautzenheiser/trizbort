using System;
using System.Drawing.Drawing2D;

namespace Trizbort.Extensions
{
  public static class RoomExtensions
  {
    public static DashStyle ConvertToDashStyle(this BorderDashStyle cxx)
    {
      switch (cxx)
      {
        case BorderDashStyle.Solid:
          return DashStyle.Solid;
        case BorderDashStyle.Dot:
          return DashStyle.Dot;
        case BorderDashStyle.Dash:
          return DashStyle.Dash;
        case BorderDashStyle.DashDot:
          return DashStyle.DashDot;
        case BorderDashStyle.DashDotDot:
          return DashStyle.DashDotDot;
        case BorderDashStyle.Custom:
        case BorderDashStyle.None:
          return DashStyle.Custom;
        default:
          throw new ArgumentOutOfRangeException("cxx", cxx, null);
      }
    }
  }
}
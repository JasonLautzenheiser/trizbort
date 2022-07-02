using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Trizbort.Util; 

internal class Smoothing : IDisposable {
  public Smoothing(Graphics graphics, SmoothingMode mode) {
    Graphics = graphics;
    SmoothingMode = graphics.SmoothingMode;
    graphics.SmoothingMode = mode;
  }

  public Graphics Graphics { get; }
  public SmoothingMode SmoothingMode { get; }

  public void Dispose() {
    Graphics.SmoothingMode = SmoothingMode;
  }
}
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Trizbort.UI.Controls {
  public class TrizbortToolTip : ToolTip {
    
    public string TitleText { get; set; }
    public string BodyText { get; set; }
    public string FooterText { get; set; }
    public bool IsShown { get; set; }
    public IWin32Window LastOwner { get; set; }
    public Point LastPosition { get; private set; }
    public object HoverElement { get; set; }

    public Color GradientColor { get; set; } = Color.Empty;

    private readonly Font bodyFont;
    private readonly Font headerFont;
    private readonly Font footerFont;
    private const int TIP_WIDTH = 200;
    private const int LINE_BUFFER = 5;
    private int tipHeight;
    private int headerHeight = 0;
    private int footerHeight = 0;
    private int bodyHeight = 0;
    double headerBottom;

    
    public TrizbortToolTip() {
      OwnerDraw = true;
      Popup += OnPopup;
      Draw += OnDraw;
      ForeColor = Color.Black;
      BackColor = Color.LightBlue;
      bodyFont = new Font("Arial", 8.0f, FontStyle.Regular);
      headerFont = new Font("Arial", 8.0f, FontStyle.Bold);
      footerFont = new Font("Arial", 8.0f, FontStyle.Bold);
    }

    private void OnPopup(object sender, PopupEventArgs e) {
      headerHeight = 0;
      footerHeight = 0;
      bodyHeight = 0;

      Image fakeImage = new Bitmap(1,1);
      Graphics graphics = Graphics.FromImage(fakeImage);

      //calc header size
      SizeF headerStringSize = graphics.MeasureString(TitleText, headerFont);
      
      //calc body size
      SizeF bodyStringSize = graphics.MeasureString(BodyText, bodyFont);

      //calc footer size
      SizeF footerStringSize = graphics.MeasureString(FooterText, footerFont);

      headerBottom = Math.Ceiling(headerStringSize.Width/TIP_WIDTH * (headerFont.Height + LINE_BUFFER));
      headerHeight = Convert.ToInt32(headerBottom) + (int)headerStringSize.Height + LINE_BUFFER;
      if (FooterText != string.Empty)
        footerHeight = Convert.ToInt32(Math.Ceiling(footerStringSize.Width/TIP_WIDTH * (footerFont.Height + LINE_BUFFER))) + (int)footerStringSize.Height + LINE_BUFFER;

      if (BodyText != string.Empty)
        bodyHeight = Convert.ToInt32(Math.Ceiling(bodyStringSize.Width/TIP_WIDTH * (bodyFont.Height + LINE_BUFFER))) + (int)bodyStringSize.Height + LINE_BUFFER;


      tipHeight = headerHeight + bodyHeight + footerHeight;
       
      e.ToolTipSize = new Size(TIP_WIDTH, tipHeight);
    }

    private void OnDraw(object sender, DrawToolTipEventArgs e) {
      var g = e.Graphics;

      Brush b;
      if (GradientColor != Color.Empty) {
        b = new LinearGradientBrush(e.Bounds,BackColor, GradientColor, 45f);
      } else {
        b = new SolidBrush(BackColor);
      }

      var textBrush = new SolidBrush(ForeColor);
      g.FillRectangle(b, e.Bounds);

      // draw header
      float titleBoundsY = 0;
      if (TitleText != string.Empty) {
        var titleBounds = new RectangleF(new PointF(e.Bounds.X + LINE_BUFFER, e.Bounds.Y + LINE_BUFFER), new SizeF(TIP_WIDTH-20,headerHeight));
        g.DrawString(TitleText, headerFont, textBrush, titleBounds); // top layer
        titleBoundsY = titleBounds.Y;
      }
      
      // draw body
      if (BodyText != string.Empty) {
        var bodyBounds = new RectangleF(new PointF(e.Bounds.X + LINE_BUFFER + 10, titleBoundsY + headerFont.Height + 6), new SizeF(TIP_WIDTH-20,bodyHeight));
        g.DrawString(BodyText, bodyFont, textBrush, bodyBounds); // top layer
      }

      // draw footer
      if (FooterText != string.Empty) {
        var footerBounds = new RectangleF(new PointF(e.Bounds.X + LINE_BUFFER, e.Bounds.Y + headerHeight + bodyHeight + LINE_BUFFER), new SizeF(TIP_WIDTH-20,footerHeight));
        g.DrawLine(new Pen(Color.Gray), new PointF(0f, footerBounds.Y), new PointF(TIP_WIDTH, footerBounds.Y));
        g.DrawString(FooterText, footerFont, textBrush, new RectangleF(new PointF(footerBounds.Location.X, footerBounds.Location.Y+2), footerBounds.Size)); 
      }

      b.Dispose();
    }

    public bool IsPositionChanged(Point position) {
      return position.X != LastPosition.X || position.Y != LastPosition.Y;
    }

    public new void Show(string text, IWin32Window window) {
      LastOwner = window;
      base.Show(text, window);
      IsShown = true;
    }

    public new void Show(string text, IWin32Window window, int duration) {
      LastOwner = window;
      base.Show(text, window, duration);
      IsShown = true;
    }

    public new void Show(string text, IWin32Window window, Point point) {
      LastOwner = window;
      LastPosition = point;
      base.Show(text, window, point);
      IsShown = true;
    }

    public new void Show(string text, IWin32Window window, int x, int y) {
      LastOwner = window;
      LastPosition = new Point(x, y);
      base.Show(text, window, x, y);
      IsShown = true;
    }

    public new void Show(string text, IWin32Window window, Point point, int duration) {
      LastOwner = window;
      LastPosition = point;
      base.Show(text, window, point, duration);
      IsShown = true;
    }

    public new void Show(string text, IWin32Window window, int x, int y, int duration) {
      LastOwner = window;
      LastPosition = new Point(x, y);
      base.Show(text, window, x, y, duration);
      IsShown = true;
    }

    public new void Hide(IWin32Window win) {
      LastOwner = null;    // not really necessary
      HoverElement = null; // as above - this one is being set outside of the class
      base.Hide(win);
      IsShown = false;
    }
  }
}
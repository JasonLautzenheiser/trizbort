using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Trizbort.Domain.Misc {
  internal class TextBlock {
    public static int s_rebuildCount;
    private readonly List<string> m_lines = new List<string>();
    private StringFormat m_actualFormat;

    private Vector m_delta;

    // cached layout data to speed drawing
    private bool m_invalidLayout = true;
    private float m_lineHeight;
    private Vector m_origin;
    private Vector m_pos;
    private StringFormat m_requestedFormat;
    private Vector m_size;
    private SizeF m_sizeChecker;
    private string m_text = string.Empty;

    public static int RebuildCount => s_rebuildCount;

    public string Text {
      get => m_text;
      set {
        m_text = value;
        m_invalidLayout = true;
      }
    }

    /// <summary>
    ///   Draw a multi-line string as it would be drawn by GDI+.
    ///   Compensates for issues and draw-vs-PDF differences in PDFsharp.
    /// </summary>
    /// <param name="graphics">The graphics with which to draw.</param>
    /// <param name="font">The font with which to draw.</param>
    /// <param name="brush">The brush with which to draw.</param>
    /// <param name="pos">The position at which to draw.</param>
    /// <param name="size">The size to which to limit the drawn text; or Vector.Zero for no limit.</param>
    /// <param name="format">The string format to use.</param>
    /// <param name="maxObjects"></param>
    /// <param name="text">The text to draw, which may contain line breaks.</param>
    /// <remarks>
    ///   PDFsharp cannot currently render multi-line text to PDF files; it comes out as single line.
    ///   This method simulates standard Graphics.DrawString() over PDFsharp.
    ///   It always has the effect of StringFormatFlags.LineLimit (which PDFsharp does not support).
    /// </remarks>
    public Rect Draw(Graphics graphics, Font font, Brush brush, Vector pos, Vector size, StringFormat format) {
      // do a quick test to see if text is going to get drawn at the same size as last time;
      // if so, assume we don't need to recompute our layout for that reason.
      var sizeChecker = graphics.MeasureString("M q", font);
            if (sizeChecker != m_sizeChecker ||
                pos != m_pos ||
                m_size != size ||
                m_requestedFormat.Alignment != format.Alignment ||
                m_requestedFormat.LineAlignment != format.LineAlignment)
            {
                m_invalidLayout = true;
            }

            // TODO: removed || m_requestedFormat.FormatFlags != format.FormatFlags from if check based on error 
            // error CS1061: 'StringFormat' does not contain a definition for 'FormatFlags'
            // This seems relevant : https://forum.pdfsharp.net/viewtopic.php?f=2&t=3898

            m_sizeChecker = sizeChecker;

      if (m_invalidLayout) {
        // something vital has changed; rebuild our cached layout data
        RebuildCachedLayout(graphics, font, ref pos, ref size, format);
        m_invalidLayout = false;
      }

      var state = graphics.Save();
      var textRect = new RectangleF(pos.X, pos.Y, size.X, size.Y);
      if (size != Vector.Zero) graphics.IntersectClip(textRect);

      // disable smoothing whilst rendering text;
      // visually this is no different, but is faster
      var smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.HighSpeed;

      var origin = m_origin;
      foreach (var t in m_lines) {
        if (size.Y > 0 && size.Y < m_lineHeight)
          break; // not enough remaining vertical space for a whole line

        var line = t;

        graphics.SmoothingMode = SmoothingMode.HighQuality;


        graphics.DrawString(line, font, brush, origin.X, origin.Y, m_actualFormat);
        origin += m_delta;
        size.Y -= m_lineHeight;
      }

      graphics.SmoothingMode = smoothingMode;
      graphics.Restore(state);

      var actualTextRect = new Rect(pos.X, m_origin.Y, size.X, m_lineHeight * m_lines.Count);
      return actualTextRect;
    }

    private void RebuildCachedLayout(Graphics graphics, Font font, ref Vector pos, ref Vector size, StringFormat baseFormat) {
      // for diagnostic purposes
      ++s_rebuildCount;

      // store current settings to help us tell if we need a rebuild next time around
      m_requestedFormat = new StringFormat();
      m_requestedFormat.Alignment = baseFormat.Alignment;
      // TODO: m_requestedFormat.FormatFlags = baseFormat.FormatFlags;
      m_requestedFormat.LineAlignment = baseFormat.LineAlignment;
      m_actualFormat = new StringFormat();
      m_actualFormat.Alignment = baseFormat.Alignment;
      // TODO: m_actualFormat.FormatFlags = baseFormat.FormatFlags;
      m_actualFormat.LineAlignment = baseFormat.LineAlignment;
      m_pos = pos;
      m_size = size;

      var text = m_text;
      if (text.IndexOf('\n') == -1 && size.X > 0 && size.Y > 0 && graphics.MeasureString(text, font).Width > size.X) {
        // wrap single-line text to fit in rectangle

        // measure a space, countering the APIs unwillingness to measure spaces
        var spaceLength = (float) (graphics.MeasureString("M M", font).Width - graphics.MeasureString("M", font).Width * 2);
        var hyphenLength = (float) graphics.MeasureString("-", font).Width;

        var wordsStep1 = new List<Word>();
        foreach (var word in text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)) {
          if (wordsStep1.Count != 0) wordsStep1.Add(new Word(" ", spaceLength));
          wordsStep1.Add(new Word(word, (float) graphics.MeasureString(word, font).Width));
        }

        bool isSplitDash = Setup.Settings.WrapTextAtDashes;
        var words = new List<Word>();
        foreach (var splits in wordsStep1.Where(p => !string.IsNullOrWhiteSpace(p.Text)).Select(word => isSplitDash ? word.Text.Split('-') : new string[] { word.Text }))
          if (splits.Count() > 1) {
            var tWordList = new List<Word>();
            foreach (var tWord in splits) {
              if (words.Count != 0 && tWordList.Count == 0)
                tWordList.Add(new Word(" ", spaceLength));
              else if (tWordList.Count != 0 && isSplitDash)
                tWordList.Add(new Word("-", hyphenLength));

              tWordList.Add(new Word(tWord, (float) graphics.MeasureString(tWord, font).Width));
            }

            words.AddRange(tWordList);
          } else {
            if (words.Count != 0)
              words.Add(new Word(" ", spaceLength));
            words.Add(new Word(splits[0], (float) graphics.MeasureString(splits[0], font).Width));
          }

        var lineLength = 0.0f;
        var total = string.Empty;
        var line = string.Empty;

        foreach (var word in words)
          if (word.Text != " " && word.Length > Math.Max(0, size.X - lineLength) && lineLength > 0) {
            if (line.Length > 0) {
              if (total.Length > 0) total += "\n";
              total += line;
              lineLength = word.Length + spaceLength;
              line = word.Text;
            }
          } else {
            line += word.Text;
            lineLength += word.Length + spaceLength;
          }

        if (line.Length > 0) {
          if (total.Length > 0) total += "\n";
          total += line;
        }

        text = total;
      }

      m_lineHeight = font.GetHeight();

      m_lines.Clear();
      m_lines.AddRange(text.Split('\n'));

      switch (m_actualFormat.LineAlignment) {
        case StringAlignment.Near:
          m_origin = pos;
          m_delta = new Vector(0, m_lineHeight);
          break;
        case StringAlignment.Far:
          m_origin = new Vector(pos.X, pos.Y + size.Y - m_lineHeight);
          if (size.Y > 0) {
            var count = m_lines.Count;
            while (m_origin.Y - m_lineHeight >= pos.Y && --count > 0) m_origin.Y -= m_lineHeight;
          } else {
            m_origin.Y -= (m_lines.Count - 1) * m_lineHeight;
          }

          m_delta = new Vector(0, m_lineHeight);
          break;
        case StringAlignment.Center:
          m_origin = new Vector(pos.X, pos.Y + size.Y / 2 - (m_lines.Count - 1) * m_lineHeight / 2 - m_lineHeight / 2);
          m_delta = new Vector(0, m_lineHeight);
          break;
      }

      m_actualFormat.LineAlignment = StringAlignment.Near;

      switch (m_actualFormat.Alignment) {
        case StringAlignment.Far:
          m_origin.X = pos.X + size.X;
          break;
        case StringAlignment.Center:
          m_origin.X = pos.X + size.X / 2;
          break;
      }
    }

    private struct Word {
      public readonly float Length;
      public readonly string Text;

      public Word(string text, float length) {
        Text = text;
        Length = length;
      }
    }
  }
}
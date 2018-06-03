using System.Drawing;
using System.Windows.Forms;
using Trizbort.Util;

namespace Trizbort.Domain.StatusBar {
  public class NumLockStatusWidget : IStatusWidget {
    public StatusItems Id => StatusItems.tsb_CapsLock;
    public string Name => "NUM Lock";
    public string MenuName => "NUM Lock State";
    public string HelpText => "NUM Lock State, Left click to change.";

    public Color DisplayColor => Control.IsKeyLocked(Keys.NumLock) ? Color.Black : Color.DarkGray;

    public string DisplayText() {
      return "NUM";
    }

    public void ClickHandler() {
      KeyboardHelper.SetNumLockKey(!Control.IsKeyLocked(Keys.NumLock));
    }




  }
}
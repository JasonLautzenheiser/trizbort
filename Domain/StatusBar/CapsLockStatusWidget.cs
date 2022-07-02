using System.Drawing;
using System.Windows.Forms;
using Trizbort.Util;

namespace Trizbort.Domain.StatusBar; 

public class CapsLockStatusWidget : IStatusWidget {
  public StatusItems Id => StatusItems.tsb_CapsLock;
  public string Name => "CAPS Lock";
  public string MenuName => "Caps Lock State";
  public string HelpText => "Caps Lock State, Left click to change.";

  public Color DisplayColor => Control.IsKeyLocked(Keys.CapsLock) ? Color.Black : Color.DarkGray;

  public string DisplayText() {
    return "CAPS";
  }

  public void ClickHandler() {
    KeyboardHelper.SetCapsLockKey(!Control.IsKeyLocked(Keys.CapsLock));
  }




}
using System.Windows.Forms;

namespace Trizbort.Util {
  public static class ClipboardHelper {
    public static bool HasSomethingToPaste() {
      return !string.IsNullOrEmpty(Clipboard.GetText());
    }
  }
}
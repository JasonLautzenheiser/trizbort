using System.Windows.Forms;

namespace Trizbort.Util {
  public static class ClipboardHelper {
    public static bool HasSomethingToPaste() {
      if (string.IsNullOrEmpty(Clipboard.GetText()))
        return false;

      return true;
    }
  }
}
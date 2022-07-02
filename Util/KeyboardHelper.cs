using System.Runtime.InteropServices;

namespace Trizbort.Util; 

public static class KeyboardHelper {
  private const byte VK_SCROLL = 0x91;
  const int VK_CAPSLOCK = 0x14;
  const int VK_NUMLOCK = 0x90;
  private const uint KEYEVENTF_KEYUP = 0x2;

    
  [DllImport("user32.dll", EntryPoint="keybd_event", SetLastError=true)]
  static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

  [DllImport("user32.dll", EntryPoint = "GetKeyState", SetLastError = true)]
  static extern short GetKeyState(uint nVirtKey);

  public static void SetScrollLockKey(bool newState)
  {
    bool scrollLockSet = GetKeyState(VK_SCROLL) != 0;
    if (scrollLockSet != newState)
    {
      keybd_event(VK_SCROLL, 0, 0, 0);
      keybd_event(VK_SCROLL, 0, KEYEVENTF_KEYUP, 0);
    }
  }
    
  public static void SetCapsLockKey(bool newState) {
    bool capsLockSet = GetKeyState(VK_CAPSLOCK) != 0;
    if (capsLockSet != newState)
    {
      keybd_event(VK_CAPSLOCK, 0, 0, 0);
      keybd_event(VK_CAPSLOCK, 0, KEYEVENTF_KEYUP, 0);
    }
  }    
    
  public static void SetNumLockKey(bool newState) {
    bool numLockSet = GetKeyState(VK_NUMLOCK) != 0;
    if (numLockSet != newState)
    {
      keybd_event(VK_NUMLOCK, 0, 0, 0);
      keybd_event(VK_NUMLOCK, 0, KEYEVENTF_KEYUP, 0);
    }
  }
}
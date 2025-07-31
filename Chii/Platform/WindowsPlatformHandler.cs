using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chii.Platform
{
    class WindowsPlatformHandler : IPlatformHandler
    {
        public event EventHandler<EventArgs> ClipboardChanged;

        private readonly Window window;

        public WindowsPlatformHandler(Window window)
        {
            this.window = window;
        }


        public void Initialize()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) throw new PlatformNotSupportedException();

            AddClipboardFormatListener(window.TryGetPlatformHandle().Handle);
            Win32Properties.AddWndProcHookCallback(window, WndProc);
        }

        public void SetKeyboardHooks()
        {
            throw new NotImplementedException();
        }

        public void UnsetKeyboardHooks()
        {
            throw new NotImplementedException();
        }

        private const UInt32 WM_CLIPBOARDUPDATE = 0x031D;

        private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                ClipboardChanged?.Invoke(this, EventArgs.Empty);
                handled = true;
            }

            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        private static extern bool AddClipboardFormatListener(IntPtr hWnd);
    }
}

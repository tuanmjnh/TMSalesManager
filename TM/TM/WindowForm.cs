using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public class DisableMovingWindows
    {
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;
        public System.Windows.Window window = null;
        public DisableMovingWindows(System.Windows.Window window)
        {
            this.window = window;
        }
        public void Window_SourceInitialized(object sender, EventArgs e)
        {
            System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(window);
            System.Windows.Interop.HwndSource source = System.Windows.Interop.HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }
    }
    public class ColorBrush
    {
        public static System.Windows.Media.SolidColorBrush Brush(string hexColor)
        {
            try
            {
                return (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFromString("#" + hexColor.Trim('#'));
            }
            catch (Exception)
            {
                return (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFromString("#000");
            }
        }
    }
    public static class WindowForm
    {
        public static void setWindowSize(this System.Windows.Window w, double width, double height, System.Windows.ResizeMode Resize = System.Windows.ResizeMode.NoResize, int Location = 0)
        {
            w.ResizeMode = Resize;
            w.Height = height;
            w.Width = width;
            //w.MinHeight = height;
            //w.MaxHeight = height;
            //w.MinWidth = width;
            //w.MaxWidth = width;

            if (Location == 1)
                w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            else if (Location == 2)
                w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            else
                w.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
        }
        public static void setWindowSize(this System.Windows.Window w, double width, double height, int Location = 0)
        {
            w.setWindowSize(width, height, System.Windows.ResizeMode.CanResize, 0);
        }
    }

}

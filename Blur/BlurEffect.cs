using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Calculate;

namespace Calculate
{
    public class WindowBlureffect
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        private uint _blurOpacity;
        public double BlurOpacity
        {
            get { return _blurOpacity; }
            set { _blurOpacity = (uint)value; EnableBlur(); }
        }

        public uint _blurBackgroundColor = 0x990000;

        public Window? window { get; set; }
        public AccentState accentState { get; set; }
        public void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(window);
            var accent = new AccentPolicy();


            //To  enable blur the image behind the window
            accent.AccentState = accentState;
            accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF); /*(White mask 0xFFFFFF)*/


            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        //to call blur in our desired window
        public WindowBlureffect(Window window, AccentState accentState)
        {
            this.window = window;
            this.accentState = accentState;
            EnableBlur();
        }
    }
}

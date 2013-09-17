using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Application = System.Windows.Application;

namespace ContextNotes
{
    public class GlobalHotkey : NativeWindow 
    {
        private const int WM_HOTKEY = 0x0312;
        private const int WM_DESTROY = 0x0002;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Dictionary<Keys, EventHandler> Actions = new Dictionary<Keys, EventHandler>();

        public GlobalHotkey()
        {
            this.CreateHandle(new CreateParams());
            Application.Current.Exit += new ExitEventHandler(Application_ApplicationExit);
        }

        public void RegisterAction(Keys key, EventHandler action)
        {
            if (Actions.ContainsKey(key))
                Actions[key] = action;
            else
            {
                Actions.Add(key, action);
                RegisterHotKey(this.Handle, 1000 + (int) key, 1, (int)key);
            }
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.DestroyHandle();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_HOTKEY:

                    
                    break;

                case WM_DESTROY: // fires when "Application.Exit();" is called
                    foreach (var action in Actions)
                        UnregisterHotKey(this.Handle, (int)action.Key);
                    
                    break;
            }
            base.WndProc(ref m);
        }
    }
}

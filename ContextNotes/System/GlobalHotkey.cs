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
    public class GlobalHotkey : NativeWindow, IDisposable
    {
        private int _currentId = 0;

        private const int WM_HOTKEY = 0x0312;
        private const int WM_DESTROY = 0x0002;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Dictionary<KeyCombination, EventHandler> Actions = new Dictionary<KeyCombination, EventHandler>();

        public GlobalHotkey()
        {
            this.CreateHandle(new CreateParams());
            Application.Current.Exit += new ExitEventHandler(Application_ApplicationExit);
        }

        public void RegisterAction(ModifierKeys modifier, Keys key, EventHandler action)
        {
            var keyComb = new KeyCombination { mod = modifier, key = key };

            if (Actions.ContainsKey(keyComb))            
                Actions[keyComb] = action;              
            else
            {                
                Actions.Add(keyComb, action);
                RegisterHotKey(this.Handle, _currentId, (uint)modifier, (uint)key);                
                _currentId++;
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
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    var keyComb = new KeyCombination { mod = modifier, key = key };

                    if (Actions.ContainsKey(keyComb))                    
                        Actions[keyComb](this, null);                    
                    
                    break;
            }
            base.WndProc(ref m);
        }

        public void Dispose()
        {
            for (int i = 0; i < _currentId; i ++ )
                UnregisterHotKey(this.Handle, i);
            
            this.DestroyHandle();
        }
    }
}

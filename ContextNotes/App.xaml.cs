using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ContextNotes;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace ContextNotes
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TrayIcon icon;
        private GlobalHotkey hotkeys;

        private NoteWindow window;
        
        public App()
            :base()        
        {
            icon = new TrayIcon();
            hotkeys = new GlobalHotkey();
            hotkeys.RegisterAction(ModifierKeys.Alt, Keys.A,
                (sender, args) =>
                {
                    if (window == null)
                    {
                        window = new NoteWindow();
                        window.Show();                        
                    }
                    else
                    {
                        window.Hide();                        
                        window = null;
                    }
                });
            hotkeys.RegisterAction(ModifierKeys.Alt, Keys.S, (sender, args) => MessageBox.Show("s"));
            
        }
    }
}

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
using ContextNotes.Model;
using System.IO;
using System.Runtime.Serialization.Json;
using ContextNotes.Serializer;

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

        private static string dataFileName = "data.json";
        
        public App()
            :base()        
        {
            icon = new TrayIcon();
            hotkeys = new GlobalHotkey();
            
            hotkeys.RegisterAction(ModifierKeys.Alt, Keys.A, ToggleWindowAction);                
        }

        private void ToggleWindowAction(object sender, EventArgs args)
        {
            if (window == null)
            {
                window = new NoteWindow();

                var procName = ProcessInfo.GetActiveProcessName();
                window.Name = procName;

                var list = JSONHelper.JsonDeserialize<List<Note>>(dataFileName);
                window.Notes = FilterNotes(procName, list);

                window.Show();
            }
            else
            {
                CloseNoteWindow();
            }
        }

        public void CloseNoteWindow()
        {
            if (window == null) return;

            window.Hide();
            JSONHelper.JsonSerialize<IEnumerable<Note>>(window.Notes, dataFileName);
            window = null;
        }

        private List<Note> FilterNotes(string term, List<Note> list)
        {           
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                //DEBUG
                if (item.Parent == null) continue;

                if (!item.Parent.Contains(term))
                {
                    list.Remove(item);
                    i--;
                }
            }

            return list;
        }
    }
}

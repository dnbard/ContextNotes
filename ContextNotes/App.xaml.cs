using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Application = System.Windows.Application;
using ContextNotes.Model;
using ContextNotes.Serializer;

namespace ContextNotes
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TrayIcon _icon;
        private readonly GlobalHotkey _hotkeys;

        public NoteWindow Window;
        public bool WindowOpened = false;

        private const string DataFileName = "data.json";

        public App()
            : base()
        {
            _icon = new TrayIcon();
            _hotkeys = new GlobalHotkey();

            _hotkeys.RegisterAction(ModifierKeys.Alt, Keys.A, ToggleWindowAction);
            CreateNoteWindow();
        }

        private void ToggleWindowAction(object sender, EventArgs args)
        {
            if (WindowOpened) CloseNoteWindow();
            else OpenNoteWindow();
        }

        private void OpenNoteWindow()
        {
            var procName = ProcessInfo.GetActiveProcessName();
            Window.Name = procName;

            var list = JSONHelper.JsonDeserialize<List<Note>>(DataFileName);
            Window.Notes = list;

            Window.Show();
            WindowOpened = true;
        }

        public void CreateNoteWindow()
        {
            Window = new NoteWindow();
            Window.Show();
            Window.Hide();
        }

        public void CloseNoteWindow()
        {
            if (Window == null) return;

            Window.Hide();
            WindowOpened = false;

            JSONHelper.JsonSerialize<IEnumerable<Note>>(Window.Notes, DataFileName);
        }
    }
}

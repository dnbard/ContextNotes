using ContextNotes.Controls;
using ContextNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContextNotes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {
        public NoteWindow()
        {
            InitializeComponent();

            NotesListChanged += NoteWindow_NotesListChanged;
        }

        public string Name { get; set; }

        private static void NoteWindow_NotesListChanged(object sender, EventArgs e)
        {
            NoteWindow self = sender as NoteWindow;
            if (self == null) return;

            var holder = self.notesHolder.Children;
            holder.Clear();

            var notesList = self.Notes;

            foreach (var note in notesList)
            {
                var control = new DragableControl();
                control.Note = note;                
                holder.Add(control);
            }
        }

        public event EventHandler NotesListChanged;
        private IEnumerable<Note> _notes;
        public IEnumerable<Note> Notes 
        {
            get { return _notes; }
            set {
                _notes = value;
                if (NotesListChanged != null) NotesListChanged(this, null);
            }
        }        
    }
}

using System.Collections.ObjectModel;
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
            DataContext = this;

            NotesListChanged += NoteWindow_NotesListChanged;
        }       
 
        public new string Name
        {
            get
            {
                if (string.IsNullOrEmpty(base.Name)) return "<default>";
                return base.Name;
            }
            set
            {
                base.Name = value;
                if (NotesListChanged != null) NotesListChanged(this, null);
            }
        }

        private readonly ObservableCollection<string> otherTerms = new ObservableCollection<string>();
        public ObservableCollection<string> OtherTerms
        {
            get { return otherTerms; }
        }

        private void NoteWindow_NotesListChanged(object sender, EventArgs e)
        {
            NoteWindow self = sender as NoteWindow;
            if (self == null) return;

            var holder = self.notesHolder.Children;
            holder.Clear();
            self.otherTerms.Clear();

            var notesList = self.Notes;
            if (notesList == null || notesList.Count == 0) return;
            var term = self.Name;

            foreach (var note in notesList)
            {
                if (!note.Parent.Contains(term))
                {
                    if (!self.otherTerms.Contains(note.Parent))
                        self.otherTerms.Add(note.Parent);
                    continue;
                }
                var control = new DragableControl();
                control.Note = note;                
                holder.Add(control);
            }

            TermsContextMenu.GetBindingExpression(ContextMenu.ItemsSourceProperty).UpdateTarget();
        }

        public event EventHandler NotesListChanged;
        private List<Note> _notes;
        public List<Note> Notes 
        {
            get { return _notes; }
            set {
                _notes = value;
                if (NotesListChanged != null) NotesListChanged(this, null);
            }
        }

        public void RemoveNote(DragableControl control)
        {
            var holder = this.notesHolder.Children;
            holder.Remove(control);

            this.Notes.Remove(control.Note);
        }

        public void AddDefaultNote()
        {
            var holder = this.notesHolder.Children;

            var note = new Note();
            note.Parent = this.Name;
            this.Notes.Add(note);            

            var control = new DragableControl();
            SetOnCenter(note);
            control.Note = note;
            holder.Add(control);
            control.Focus();
        }

        private void SetOnCenter(Note note)
        {
            var width = this.Width;
            var height = this.Height;

            note.X = width * 0.5 - 150;
            note.Y = height * 0.5 - 250;
        }

        private void ChangeTerm(object sender, RoutedEventArgs e)
        {
            var self = sender as MenuItem;
            if (self == null) return;

            var newTerm = self.Header as String;
            if (string.IsNullOrEmpty(newTerm)) return;

            this.Name = newTerm;
        }

        private void ShowContextMenu(object sender, MouseButtonEventArgs e)
        {
            TermsContextMenu.PlacementTarget = this;
            TermsContextMenu.IsOpen = true;
        }
    }
}

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

namespace ContextNotes.Controls
{
    /// <summary>
    /// Interaction logic for DragableControl.xaml
    /// </summary>
    public partial class DragableControl : UserControl
    {
        object MovingObject = null;
        double FirstXPos, FirstYPos;        

        public DragableControl()
        {
            InitializeComponent();            
        }

        private Note _note = null;
        public Note Note 
        {
            get { return _note; }
            set
            {
                _note = value;
                DataContext = value;
                Init();
            }
        }

        private void Init()
        {
            this.SetValue(Canvas.LeftProperty, Note.X);
            this.SetValue(Canvas.TopProperty, Note.Y);
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                        sender == MovingObject)
            {
                var control = sender as FrameworkElement;
                var parent = control.Parent as FrameworkElement;
                var position = e.GetPosition((Panel)(sender as FrameworkElement).Parent);

                var x = position.X - FirstXPos - 25;
                var y = position.Y - FirstYPos - 25;

                control.SetValue(Canvas.LeftProperty, x);
                control.SetValue(Canvas.TopProperty, y);

                Note.X = x;
                Note.Y = y;
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as FrameworkElement;
            var position = e.GetPosition(control);
            FirstXPos = position.X;
            FirstYPos = position.Y;
            MovingObject = sender;
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MovingObject = null;
        }
    }
}

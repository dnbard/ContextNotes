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
        }

        object MovingObject = null;
        double FirstXPos, FirstYPos;
        double FirstArrowXPos, FirstArrowYPos;

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                        sender == MovingObject)
            {
                var control = sender as FrameworkElement;
                var parent = control.Parent as FrameworkElement;

                control.SetValue(Canvas.LeftProperty,
                     e.GetPosition((Panel)(sender as FrameworkElement).Parent).X - FirstXPos - 25);

                control.SetValue(Canvas.TopProperty,
                     e.GetPosition((Panel)(sender as FrameworkElement).Parent).Y - FirstYPos - 25);
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as FrameworkElement;
            FirstXPos = e.GetPosition(control).X;
            FirstYPos = e.GetPosition(control).Y;
            MovingObject = sender;
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MovingObject = null;
        }
    }
}

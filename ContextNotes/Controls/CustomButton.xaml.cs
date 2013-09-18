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
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        public ImageSource Source
        {
            get { return Image.Source; }
            set { Image.Source = value; }
        }

        public string Behaviour { get; set; }

        private void mouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(Behaviour)) return;
            var Application = App.Current as App;
            if (Application == null) return;

            switch (Behaviour)
            {
                case "Exit": Application.CloseNoteWindow(); break;
                case "Add": Application.Window.AddDefaultNote(); break;                
            }
        }
    }
}

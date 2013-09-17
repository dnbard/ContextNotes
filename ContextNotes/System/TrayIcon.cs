using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContextNotes
{
    public class TrayIcon
    {
        private static readonly TrayIcon _inst = new TrayIcon();
        public static TrayIcon Instance
        {
            get { return _inst; }
        }

        private readonly NotifyIcon icon;
        public string Text
        {
            get { return icon.Text; }
            set { icon.Text = value; }
        }

        private TrayIcon()
        {
            icon = new NotifyIcon();
            icon.Icon = new Icon("cn.ico");
            icon.Visible = true;
            icon.Text = "Context Notes";
        }
    }
}

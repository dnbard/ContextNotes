using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContextNotes
{
    public class TrayIcon
    {
        private readonly NotifyIcon icon = new NotifyIcon();
        private string iconName = "cn.ico";
        
        public string Text
        {
            get { return icon.Text; }
            set { icon.Text = value; }
        }             

        public TrayIcon()
        {
            Text = "Context Notes";         

            CheckIcon(this);
            if (!string.IsNullOrEmpty(iconName))
                icon.Icon = new Icon(iconName);
            icon.Visible = true;
            
        }

        private static void CheckIcon(TrayIcon icon)
        {
            if (!File.Exists(icon.iconName)) 
                icon.iconName = "";
        }
    }
}

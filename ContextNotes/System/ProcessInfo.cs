using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ContextNotes
{
    class ProcessInfo
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static string GetActiveProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            
            if (p == null) return "";
            return FilterProccesName(p.ProcessName);
        }

        private static string DefaultProcessName = "<default>";
        private static string[] stopWords = { "ContextNotes", "Idle" };
        private static string FilterProccesName(string name)
        {
            if (string.IsNullOrEmpty(name)) return DefaultProcessName;

            foreach (var word in stopWords)
                if (name.Contains(word)) return DefaultProcessName;
            return name;
        }
    }
}

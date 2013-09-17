using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextNotes.Model
{
    public class Note
    {
        public string Caption { get; set; }
        public string Text { get; set; }

        public string Parent { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
    }
}

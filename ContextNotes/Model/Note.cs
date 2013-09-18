using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContextNotes.Model
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Parent { get; set; }

        [DataMember]
        public double X { get; set; }
        [DataMember]
        public double Y { get; set; }
    }
}

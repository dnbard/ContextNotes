using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContextNotes.Serializer
{
    public static class JSONHelper
    {
        public static bool JsonSerialize<T>(T obj, string filename)
        {
            try
            {
                var ser = new DataContractJsonSerializer(typeof(T));

                MemoryStream ms = new MemoryStream();                
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();

                var file = new StreamWriter(filename);
                file.Write(jsonString);
                file.Close();

                return true;
            }
            catch { return false; }
        }

        public static T JsonDeserialize<T>(string filename)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                FileStream file = new FileStream(filename, FileMode.Open);

                T obj = (T)ser.ReadObject(file);

                file.Close();
                return obj;
            }
            catch { return default(T); }
        }
    }
}

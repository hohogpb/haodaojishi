using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HaoDaoJiShi.Utils
{
    public class utils
    {
        public static void ObjectToXml<T>(ref T t, string xml)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(xml))
                {
                    XmlSerializer xz = new XmlSerializer(t.GetType());
                    xz.Serialize(sw, t);
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        public static void XmltoObject<T>(ref T t, string xml)
        {
            try
            {
                using (StreamReader sr = new StreamReader(xml))
                {
                    XmlSerializer xz = new XmlSerializer(t.GetType());
                    t = (T)xz.Deserialize(sr);
                }
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        static public T JsonToObject<T>(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return Jil.JSON.Deserialize<T>(sr);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        static public void ObjectToJson<T>(T d, string path)
        {
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    Jil.JSON.Serialize(d, writer);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace DataDispacher.Logic
{
    public class SerializationHelper<T>
    {
        public static string Serialize(T lb)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    FiltratedXmlTextWriter xmlTextWriter = new FiltratedXmlTextWriter(ms, System.Text.Encoding.UTF8);
                    xs.Serialize(xmlTextWriter, lb);
                    int bomLen = 3;
                    return Encoding.UTF8.GetString(ms.GetBuffer(), bomLen, (int)(ms.Length - bomLen));
                }
                catch (Exception ex)
                {
                    throw new Exception();
                    
                }
            }
        }

        public static T Deserialize(string xmlData)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(xmlData);
                    xd.Save(ms);
                    ms.Position = 0; //rewind stream position to 0
                    return (T)xs.Deserialize(ms);
                }
                catch (Exception ex)
                {
                    throw new Exception();
                    
                }
            }
        }
    }
}
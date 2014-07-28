using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Text;


namespace DataDispacher.Logic
{
    public class FiltratedXmlTextWriter : XmlTextWriter
    {
        public FiltratedXmlTextWriter(TextWriter w) : base(w){ }

        public FiltratedXmlTextWriter(Stream w, Encoding encoding) : base(w, encoding){ }

        public FiltratedXmlTextWriter(string filename, Encoding encoding) : base(filename, encoding){ }

        internal void CheckUnicodeString(ref string value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                if (value[i] > 0xFFFD)
                {
                    value = value.Replace(value[i], '?');
                    //throw new Exception("Invalid Unicode");
                }
                else if (value[i] < 0x20 && value[i] != '\t' & value[i] != '\n' & value[i] != '\r')
                {
                    value = value.Replace(value[i], '?');
                    //throw new Exception("Invalid Xml Characters");
                }
            }
        }

        public override void WriteString(string text)
        {
            CheckUnicodeString(ref text);
            base.WriteString(text);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDispacher.Logic
{
    public class LastUpdate
    {
        private string m_LastUpdate;
        public string LASTUPDATETIME
        {
            get { return m_LastUpdate; }
            set { m_LastUpdate = value; }

        }
    }
}
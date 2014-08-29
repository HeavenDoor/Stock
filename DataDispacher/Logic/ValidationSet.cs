using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDispacher.Logic
{
    public class ValidationSet
    {

        private string m_PhoneID;
        public string PhoneID
        {
            get { return m_PhoneID; }
            set { m_PhoneID = value; }
        }

        private string m_ValidationCode;
        public string ValidationCode
        {
            get { return m_ValidationCode; }
            set { m_ValidationCode = value; }
        }

        private DateTime m_RegTime;
        public DateTime RegTime
        {
            get { return m_RegTime; }
            set { m_RegTime = value; }
        }
    }
}
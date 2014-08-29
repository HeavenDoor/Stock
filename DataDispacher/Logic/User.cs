using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDispacher.Logic
{
    public class User
    {
        private string m_username;
        public string UserName
        {
            get { return m_username; }
            set { m_username = value; }
        }

        private string m_password;
        public string PassWord
        {
            get { return m_password; }
            set { m_password = value; }
        }

        private string m_email;
        public string Email
        {
            get { return m_email; }
            set { m_email = value; }
        }

        private string m_phonenumber;
        public string PhoneNumber
        {
            get { return m_phonenumber; }
            set { m_phonenumber = value; }
        }

        private string m_phoneid;
        public string PhoneID
        {
            get { return m_phoneid; }
            set { m_phoneid = value; }
        }

        private DateTime m_regtime;
        public DateTime RegTime
        {
            get { return m_regtime; }
            set { m_regtime = value; }
        }
    }
}
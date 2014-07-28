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

        private string m_phone;
        public string Phone
        {
            get { return m_phone; }
            set { m_phone = value; }
        }
    }
}
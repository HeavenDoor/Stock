using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDispacher.Logic
{
    public class Stock
    {
        public Stock() { }
        private string name;
        public string Name
        {
            get {return name;}
            set { name = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
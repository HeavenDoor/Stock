using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSync
{
    public class Stock
    {
        private string m_StockName;
        public string StockName
        {
            get { return m_StockName; }
            set { m_StockName = value; }
        }

        private string m_StockCode;
        public string StockCode
        {
            get { return m_StockCode; }
            set { m_StockCode = value; }
        }
    }
}

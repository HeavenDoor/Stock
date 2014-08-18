using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDispacher.Logic
{
    public class StockItemResult : LogicBase
    {
        private List<StockItem> m_Stockitems = new List<StockItem>();
        public List<StockItem> StockItems
        {
            get { return m_Stockitems; }
            set { m_Stockitems = value; }
        }
    }
}
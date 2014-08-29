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

    public class Stockitem_Changerate_FluctuaterateResult : LogicBase
    {
        private List<Stockitem_Changerate_Fluctuaterate> m_Stockitem_Changerate_Fluctuaterates = new List<Stockitem_Changerate_Fluctuaterate>();
        public List<Stockitem_Changerate_Fluctuaterate> Stockitem_Changerate_Fluctuaterates
        {
            get { return m_Stockitem_Changerate_Fluctuaterates; }
            set { m_Stockitem_Changerate_Fluctuaterates = value; }
        }
    }

    public class ValidationCodeResult : LogicBase
    {
        private string m_Image;
        public string Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDispacher.Logic
{
    public class Stockitem_Changerate_Fluctuaterate
    {
        public Stockitem_Changerate_Fluctuaterate(){}
        /// <summary>
        /// 股票代码 
        /// </summary>
        private string m_StockCode;
        public string StockCode
        {
            get { return m_StockCode; }
            set { m_StockCode = value; }
        }

        /// <summary>
        /// 股票名称 
        /// </summary>
        private string m_StockName;
        public string StockName
        {
            get { return m_StockName; }
            set { m_StockName = value; }
        }

        /// <summary>
        /// 涨跌幅 
        /// </summary>
        private double m_FluctuateRate;
        public double FluctuateRate
        {
            get { return m_FluctuateRate; }
            set { m_FluctuateRate = value; }
        }

        /// <summary>
        /// 换手率 
        /// </summary>
        private double m_ChangeRate;
        public double ChangeRate
        {
            get { return m_ChangeRate; }
            set { m_ChangeRate = value; }
        }

        /// <summary>
        /// 计算换手率还是涨幅的标志 TRUE 换手率  FALSE 涨幅
        /// </summary>
        private bool m_ChangerateMain;
        public bool ChangerateMain
        {
            get { return m_ChangerateMain; }
            set { m_ChangerateMain = value; }
        }

        /// <summary>
        /// 交易日统计标识  1,2,3,5,10,15,.....
        /// </summary>
        private int m_TradeDays;
        public int TradeDays
        {
            get { return m_TradeDays; }
            set { m_TradeDays = value; }
        }

    }
}

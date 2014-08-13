using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSync
{
    public class StockItem
    {
        /// <summary>
        /// 股票交易时间  唯一 
        /// </summary>
        private DateTime m_StockDate;
        public DateTime StockDate
        {
            get { return m_StockDate; }
            set { m_StockDate = value; }
        }

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
        /// 开盘价 
        /// </summary>
        private double m_OpenPrice;
        public double OpenPrice
        {
            get { return m_OpenPrice; }
            set { m_OpenPrice = value; }
        }

        /// <summary>
        /// 收盘价 
        /// </summary>
        private double m_ClosePrice;
        public double ClosePrice
        {
            get { return m_ClosePrice; }
            set { m_ClosePrice = value; }
        }

        /// <summary>
        /// 最高价 
        /// </summary>
        private double m_HighestPrice;
        public double HighestPrice
        {
            get { return m_HighestPrice; }
            set { m_HighestPrice = value; }
        }

        /// <summary>
        /// 最低价 
        /// </summary>
        private double m_LowestPrice;
        public double LowestPrice
        {
            get { return m_LowestPrice; }
            set { m_LowestPrice = value; }
        }

        /// <summary>
        /// 涨跌额 
        /// </summary>
        private double m_FluctuateMount;
        public double FluctuateMount
        {
            get { return m_FluctuateMount; }
            set { m_FluctuateMount = value; }
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
        /// 成交量 
        /// </summary>
        private double m_TradeVolume;
        public double TradeVolume
        {
            get { return m_TradeVolume; }
            set { m_TradeVolume = value; }
        }

        /// <summary>
        /// 成交金额 
        /// </summary>
        private double m_TradeMount;
        public double TradeMount
        {
            get { return m_TradeMount; }
            set { m_TradeMount = value; }
        }

        /// <summary>
        /// 总市值 
        /// </summary>
        private double m_ToatlMarketCap;
        public double ToatlMarketCap
        {
            get { return m_TradeMount; }
            set { m_TradeMount = value; }
        }

        /// <summary>
        /// 流通市值 
        /// </summary>
        private double m_CirculationMarketCap;
        public double CirculationMarketCap
        {
            get { return m_CirculationMarketCap; }
            set { m_CirculationMarketCap = value; }
        }

        public bool Equals(StockItem o)
        {
            if (StockDate != o.StockDate)
            {
                return false;
            }
            if (StockCode != o.StockCode)
            {
                return false;
            }
            if (StockName != o.StockName)
            {
                return false;
            }
            if (m_OpenPrice != o.m_OpenPrice)
            {
                return false;
            }
            if (ClosePrice != o.ClosePrice)
            {
                return false;
            }
            if (HighestPrice != o.HighestPrice)
            {
                return false;
            }
            if (LowestPrice != o.LowestPrice)
            {
                return false;
            }
            if (FluctuateMount != o.FluctuateMount)
            {
                return false;
            }
            if (FluctuateRate != o.FluctuateRate)
            {
                return false;
            }
            if (ChangeRate != o.ChangeRate)
            {
                return false;
            }
            if ((double)TradeVolume != (double)o.TradeVolume)
            {
                return false;
            }
            if ((double)TradeMount != (double)o.TradeMount)
            {
                return false;
            }
            if ((double)ToatlMarketCap != (double)o.ToatlMarketCap)
            {
                return false;
            }
            if ((double)CirculationMarketCap != (double)o.CirculationMarketCap)
            {
                return false;
            }

            return true;
        }
    }
}

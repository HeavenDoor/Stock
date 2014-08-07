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
        private float m_OpenPrice;
        public float OpenPrice
        {
            get { return m_OpenPrice; }
            set { m_OpenPrice = value; }
        }

        /// <summary>
        /// 收盘价 
        /// </summary>
        private float m_ClosePrice;
        public float ClosePrice
        {
            get { return m_ClosePrice; }
            set { m_ClosePrice = value; }
        }

        /// <summary>
        /// 最高价 
        /// </summary>
        private float m_HighestPrice;
        public float HighestPrice
        {
            get { return m_HighestPrice; }
            set { m_HighestPrice = value; }
        }

        /// <summary>
        /// 最低价 
        /// </summary>
        private float m_LowestPrice;
        public float LowestPrice
        {
            get { return m_LowestPrice; }
            set { m_LowestPrice = value; }
        }

        /// <summary>
        /// 涨跌额 
        /// </summary>
        private float m_FluctuateMount;
        public float FluctuateMount
        {
            get { return m_FluctuateMount; }
            set { m_FluctuateMount = value; }
        }

        /// <summary>
        /// 涨跌幅 
        /// </summary>
        private float m_FluctuateRate;
        public float FluctuateRate
        {
            get { return m_FluctuateRate; }
            set { m_FluctuateRate = value; }
        }

        /// <summary>
        /// 换手率 
        /// </summary>
        private float m_ChangeRate;
        public float ChangeRate
        {
            get { return m_ChangeRate; }
            set { m_ChangeRate = value; }
        }

        /// <summary>
        /// 成交量 
        /// </summary>
        private float m_TradeVolume;
        public float TradeVolume
        {
            get { return m_TradeVolume; }
            set { m_TradeVolume = value; }
        }

        /// <summary>
        /// 成交金额 
        /// </summary>
        private float m_TradeMount;
        public float TradeMount
        {
            get { return m_TradeMount; }
            set { m_TradeMount = value; }
        }

        /// <summary>
        /// 总市值 
        /// </summary>
        private float m_ToatlMarketCap;
        public float ToatlMarketCap
        {
            get { return m_TradeMount; }
            set { m_TradeMount = value; }
        }

        /// <summary>
        /// 流通市值 
        /// </summary>
        private float m_CirculationMarketCap;
        public float CirculationMarketCap
        {
            get { return m_CirculationMarketCap; }
            set { m_CirculationMarketCap = value; }
        }
    }
}

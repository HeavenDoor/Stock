using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;

using StockCommon;

namespace StockSync
{
    public class WeekLogic
    {
        public static bool IsWeekend(DateTime dt)
        {
            bool isWeekend = false;
            switch(dt.DayOfWeek)
            {
                           
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    isWeekend = false;
                    break;
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    isWeekend = true;
                    break;
            }
            return isWeekend;
        }

        public static bool IsHoliday(DateTime dt)
        {
            bool isHoliday = false;
            string date = string.Format("{0}/{1}/{2}", dt.Year, dt.Month, dt.Day);
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            string sql = string.Format("SELECT * FROM HOLIDAYS where HOLIDAY='{0}'", date);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<HoliDays> _table = EntityReader.GetEntities<HoliDays>(table);
            if(_table.Count == 1)
            {
                isHoliday = true;
            }
            return isHoliday;
        }
    }

    public class LastUpdate
    {
        private DateTime m_LastUpdateTime;
        public DateTime LastUpdateTime
        {
            get { return m_LastUpdateTime; }
            set { m_LastUpdateTime = value; }
        }
    }

    public class HoliDays
    {
        private DateTime m_Holiday;
        public DateTime Holiday
        {
            get { return m_Holiday; }
            set { m_Holiday = value; }
        }
    }

    public class TradeDates
    {
        private DateTime m_StockTradeDate;
        public DateTime StockTradeDate
        {
            get { return m_StockTradeDate; }
            set { m_StockTradeDate = value; }
        }
    }

    public class TransactionDate
    {
        public static string GetDatesOfTransaction(int days)
        {
            DateTime today = DateTime.Now;
//             for (DateTime dt = new DateTime(2009, 12, 4); dt < new DateTime(2010, 12, 1); dt = dt.)
//             {
//                 //Response.Write(dt.ToShortDateString());
//             }
            return "";
        }

        public static string GetRecentDay()
        {
            string recentDay = string.Empty;
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            string sql = string.Format("SELECT * from TRADEDATE ORDER BY STOCKTRADEDATE DESC LIMIT 1");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<TradeDates> _table = EntityReader.GetEntities<TradeDates>(table);
            if (_table.Count != 1)
            {
                return recentDay;
            }
            DateTime dt = _table[0].StockTradeDate;
            recentDay = string.Format("{0}/{1}/{2}", dt.Year, dt.Month, dt.Day);
            return recentDay;
        }
    }
}

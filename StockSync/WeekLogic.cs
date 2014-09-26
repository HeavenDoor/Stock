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
            string destDay = string.Empty;
            string recentDay = GetRecentDay();
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            string sql = string.Format("SELECT * from TRADEDATE ORDER BY STOCKTRADEDATE DESC LIMIT {0}", days);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<TradeDates> _table = EntityReader.GetEntities<TradeDates>(table);
            DateTime dt;
            if (_table.Count == 0)
            {
                return destDay;
            }
            else if (_table.Count < days)
            {
                dt = _table[_table.Count -1].StockTradeDate;
            }
            else
            {
                dt = _table[days - 1].StockTradeDate;
            }
            destDay = string.Format("{0}/{1}/{2}", dt.Year, dt.Month, dt.Day);  
            return destDay;
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
            string month = Convert.ToString(dt.Month);
            string day = Convert.ToString(dt.Day);

            if (dt.Month < 10)
            {
                month = "0" + Convert.ToString(dt.Month);
            }
            if (dt.Day < 10)
            {
                day = "0" + Convert.ToString(dt.Day);
            }
            recentDay = string.Format("{0}/{1}/{2}", dt.Year, month, day);
            return recentDay;
        }

        public static void SyncTradeAllDate()
        {
            DateTime today = DateTime.Now;
            if (today.Hour < 17) // 17点开始更新
            {
                today = today.AddDays(-1);
            }
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            string sqlDelete = string.Format("DELETE FROM TRADEDATE");
            try
            {
                util.ExecuteNonQuery(sqlDelete, null);
            }
            catch (Exception e)
            {
                LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncTradeAllDate()>, MSG : {0} ", e.Message));
            }
           
            for (DateTime dt = new DateTime(2013, 12, 30); dt <= today; dt = dt.AddDays(1))
            {
                if (WeekLogic.IsHoliday(dt) || WeekLogic.IsWeekend(dt))
                {
                    continue;
                }
                string date = string.Format("{0}/{1}/{2}", dt.Year, dt.Month, dt.Day);
                string sql = string.Format("SELECT * FROM TRADEDATE WHERE STOCKTRADEDATE='{0}'", date);
                DataTable table = util.ExecuteDataTable(sql, null);
                List<TradeDates> values = EntityReader.GetEntities<TradeDates>(table);
                if (values.Count == 1)
                {
                    return;
                }
                else if (values.Count ==0)
                {
                    string sqlInsert = string.Format("INSERT INTO TRADEDATE VALUES('{0}')", date);
                    try
                    {
                        util.ExecuteNonQuery(sqlInsert, null);
                    }
                    catch (Exception e)
                    {
                        LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncTradeAllDate()>, MSG : {0} ", e.Message));
                    }
                }  
            }
        }

        public static void SyncTradeCurrentDate()
        {
            DateTime today = DateTime.Now;
            if (today.Hour < 17) // 17点开始更新
            {
                today = today.AddDays(-1);
            }
            if (WeekLogic.IsHoliday(today) || WeekLogic.IsWeekend(today))
            {
                return;
            }
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            string date = string.Format("{0}/{1}/{2}", today.Year, today.Month, today.Day);
            string sql = string.Format("SELECT * FROM TRADEDATE WHERE STOCKTRADEDATE='{0}'", date);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<TradeDates> values = EntityReader.GetEntities<TradeDates>(table);
            if (values.Count == 1)
            {
                return;
            }
            else if (values.Count == 0)
            {
                string sqlInsert = string.Format("INSERT INTO TRADEDATE VALUES('{0}')", date);
                try
                {
                    util.ExecuteNonQuery(sqlInsert, null);
                }
                catch (Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncTradeCurrentDate()>, MSG : {0} ", e.Message));
                }
            } 
        }
    }
}

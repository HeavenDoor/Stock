using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockCommon;
namespace StockSync
{
    class StockLogic
    {
        public static string strStockCodeFormat = "%StockCode%";
        public static string strDateBeginFormat = "%StartDate%";
        public static string strDateTodayFormat = "%EndDate%";


        public static CodeType GetCodeType(string stockcode)
        {
            if (stockcode.StartsWith("6"))
            {
                return CodeType.ShangHai;
            }
            else if (stockcode.StartsWith("00") || stockcode.StartsWith("3"))
            {
                return CodeType.ShenZhen;
            }
            else
            {
                return CodeType.UnKnown;
            }
        }

        public enum CodeType
        {   
            ShangHai = 0,
            ShenZhen = 1,
            UnKnown = 2,
        }


        /// <summary>
        /// 参数1 strStockCode  股票代码  如002560
        /// 参数2 是否同步3个月  默认同步当天
        /// </summary>
        public static string GenetateStockUrl(string strStockCode, bool isSync = false)
        {
            string stockUrlFormat = Configuration.StockItemUrl;
            CodeType type = GetCodeType(strStockCode);
            string strCode = Convert.ToString((int)type) + strStockCode;
            DateTime today = DateTime.Now;
            string recentday = TransactionDate.GetRecentDay();

            string strToday = recentday.Replace("/", "");//Convert.ToString(today.Year) + strmonth + strday;
            string strOtherDay = "";
            if (!isSync)
            {
                strOtherDay = strToday;
            }
            else
            {
                strOtherDay = GetThreeMonthToday();
            }
            stockUrlFormat = stockUrlFormat.Replace(strStockCodeFormat, strCode);
            stockUrlFormat = stockUrlFormat.Replace(strDateBeginFormat, strOtherDay);
            stockUrlFormat = stockUrlFormat.Replace(strDateTodayFormat, strToday);
            return stockUrlFormat;
        }

        private static string GetThreeMonthToday()
        {
            DateTime today = DateTime.Now;
            int year = today.Year;
            int month = today.Month;

            if (today.Month - 3 < 1)
            {
                year = year - 1;
                month = month + 9;
            }
            else
            {
                month = month - 3;
            }

            int day = today.Day;
            string strmonth = Convert.ToString(month);
            string strday = Convert.ToString(today.Day);
            if (month < 10)
            {
                strmonth = "0" + strmonth;
            }
            if (day < 10)
            {
                strday = 0 + strday;
            }

            string strOtherDay = Convert.ToString(year) + strmonth + strday;
            return strOtherDay;
        }
    }
}

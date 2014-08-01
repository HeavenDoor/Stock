using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSync
{
    class StockLogic
    {
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
            UnKnown = 0,
            ShangHai = 1,
            ShenZhen = 2,
        }

        public static string GenetateStockUrl(string strCode)
        {
            return "";
        }
    }
}

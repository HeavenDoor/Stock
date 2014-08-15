using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSync
{
    public class WeekLogic
    {
        public static bool IsWeekend()
        {
            bool isWeekend = false;
            DateTime dt = DateTime.Now;
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

        public static bool IsHoliday()
        {
            bool isHoliday = false;
            DateTime dt = DateTime.Now;

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
}

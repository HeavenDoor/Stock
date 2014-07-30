using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using StockSync;

namespace DataCollector
{
    public class TimeListenerThread
    {
        private Thread m_TimeThread = null;

        private System.Threading.Timer m_TimerSync;

        public void start()
        {
            if (m_TimeThread == null)
                m_TimeThread = new Thread(TimeListenerThreadRun);
            m_TimeThread.Start();
        }

        public void TimeListenerThreadRun()
        {
            
            while(true)
            {
                DateTime currenttime = System.DateTime.Now;
                if (currenttime.Hour == 15 && currenttime.Minute == 5 && currenttime.Second == 0)
                {
                    StockDataSync.SyncStockList();


                }
            }
        }
    }
}

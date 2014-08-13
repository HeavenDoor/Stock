using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quartz;
using StockSync;
using StockCommon;

namespace DataCollector
{
    public class StockSyncJob : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            DateTime currenttime = System.DateTime.Now;
//             if (/*currenttime.Hour == 15 &&*/ currenttime.Minute % 10 == 5 && currenttime.Second == 0)
//             {
                LogManager.WriteLog(LogManager.LogFile.Trace,
                    "*****************************************  Start Sync  *****************************************");
                //StockDataSync.SyncStockList();

                //StockDataSync.SyncStockDataDetaileList();
                LogManager.WriteLog(LogManager.LogFile.Trace,
                    "*****************************************  End Sync  *****************************************");
//            }
        }
    }
}

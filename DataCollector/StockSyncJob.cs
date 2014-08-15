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

            LogManager.WriteLog(LogManager.LogFile.Trace,
                "*****************************************  Start Sync  *****************************************");

            StockDataSync.SyncStockList(); // 更新同步股票列表

            StockDataSync.SyncStockDataDetaileList(); // 跟新同步股票日交易数据

            StockDataSync.ComputeStockSide(); // 股票边界数据逻辑处理

            StockDataSync.SyncLastUpdate(); // 更新同步时间

            LogManager.WriteLog(LogManager.LogFile.Trace,
                "*****************************************  End Sync  *****************************************");
        }
    }
}

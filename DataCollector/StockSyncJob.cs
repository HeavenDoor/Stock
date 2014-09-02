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

            StockDataSync.SyncTradeCurrentDate(); // 更新同步交易日期
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncTradeCurrentDate ***************");

            StockDataSync.SyncStockList(); // 更新同步股票列表
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncStockList ***************");

            StockDataSync.SyncStockDataDetaileList(); // 更新同步股票日交易数据
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncStockDataDetaileList ***************");

            StockDataSync.ComputeStockSide(); // 股票边界数据逻辑处理
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish ComputeStockSide ***************");


            StockDataSync.SyncLastUpdate(); // 更新同步时间
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncLastUpdate ***************");

            LogManager.WriteLog(LogManager.LogFile.Trace,
                "*****************************************  End Sync  *****************************************");
        }
    }
}

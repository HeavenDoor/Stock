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

//             while (true)
//             {
//                 int m = 0;
//             }
            DateTime currenttime = System.DateTime.Now;

            LogManager.WriteLog(LogManager.LogFile.Trace,
                "*****************************************  Start Sync  *****************************************");

            StockDataSync.SyncTradeCurrentDate(); // 更新同步交易日期
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncTradeCurrentDate ***************");

            StockDataSync.SyncStockList(); // 更新同步股票列表
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncStockList ***************");

            StockDataSync.SyncDailyTradeData();  // 更新每日股票交易数据
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncDailyTradeData ***************");




//             StockDataSync.SyncStockDataDetaileList(true); // 更新同步3个月股票日交易数据
//             LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncStockDataDetaileList ***************");
// 
//             StockDataSync.SyncStockDataDetaileList(false); // 更新同步当日股票日交易数据
//             LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncStockDataDetaileList ***************");
// 
//             StockDataSync.SyncStockDataDetaileListExt(); // 更新常规方法获取不到的股票日交易数据
//             LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncStockDataDetaileList ***************");
//             
//             
//             StockDataSync.ComputeStockSide(); // 股票边界数据逻辑处理
//             LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish ComputeStockSide ***************");


            StockDataSync.SyncLastUpdate(); // 更新同步时间
            LogManager.WriteLog(LogManager.LogFile.Trace, "********** finish SyncLastUpdate ***************");

            LogManager.WriteLog(LogManager.LogFile.Trace,
                "*****************************************  End Sync  *****************************************");
        }
    }
}

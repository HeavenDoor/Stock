using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using StockCommon;
using StockSync;

using Quartz.Impl;
using Quartz.Job;
using Quartz;

namespace DataCollector
{
    public partial class StockService : ServiceBase
    {
        public ISchedulerFactory sf;
        public IScheduler sched;
        public DateTimeOffset startTime;
        public IJobDetail job;
        public ICronTrigger trigger;
        //public ISimpleTrigger trigger;
        public DateTimeOffset? ft;
        public StockService()
        {
            
            InitializeComponent();
            StockCommon.LogManager.LogPath = System.AppDomain.CurrentDomain.BaseDirectory;  //"E:\\Users\\shenghai\\Desktop\\hrer\\";


            sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();

            //startTime = DateBuilder.DateOf(10, 30, 10);

            job = JobBuilder.Create<StockSyncJob>().WithIdentity("StockSyncJob", "StockSyncJobGroup").Build();


            trigger = (ICronTrigger)TriggerBuilder.Create().WithIdentity("StockSyncJob", "StockSyncJobGroup").WithSchedule(CronScheduleBuilder.CronSchedule("0 48 18 ? * *")).Build();

            //ISimpleTrigger trigger = TriggerUtils.MakeMinutelyTrigger(1)
            ft = sched.ScheduleJob(job, trigger);

            StockDataSync.SyncTradeAllDate();
        
        }

        protected override void OnStart(string[] args)
        {
//             while(true)
//             {
//                 int m = 0;
//             }


            sched.Start();

//              TimeListenerThread thread = new TimeListenerThread();
//              thread.start();


             StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "server start");
        }

        protected override void OnStop()
        {

            sched.Shutdown(true);



            string s = Configuration.SqlConnectStr;
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "server end");
        }
    }
}

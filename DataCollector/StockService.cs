using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using StockCommon;

namespace DataCollector
{
    public partial class StockService : ServiceBase
    {
        public StockService()
        {
            
            InitializeComponent();
            LogManager.LogPath = "E:\\Users\\shenghai\\Desktop\\hrer\\";
        }

        protected override void OnStart(string[] args)
        {
//             while(true)
//             {
//                 int m = 0;
//             }
//             TimeListenerThread thread = new TimeListenerThread();
//             thread.start();


            LogManager.WriteLog(LogManager.LogFile.Trace, "server start");
        }

        protected override void OnStop()
        {
            TimeListenerThread thread = new TimeListenerThread();
            thread.start();

            string s = Configuration.SqlConnectStr;
            LogManager.WriteLog(LogManager.LogFile.Trace, "server end");
        }
    }
}

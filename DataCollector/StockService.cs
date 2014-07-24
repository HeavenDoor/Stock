using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace DataCollector
{
    public partial class StockService : ServiceBase
    {
        public StockService()
        {
            LogManager.LogPath = "E:\\Users\\shenghai\\Desktop\\hrer\\";
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            LogManager.WriteLog(LogManager.LogFile.Trace, "server start");
        }

        protected override void OnStop()
        {
            LogManager.WriteLog(LogManager.LogFile.Trace, "server end");
        }
    }
}

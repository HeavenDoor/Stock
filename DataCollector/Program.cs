using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using StockCommon;

namespace DataCollector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var config = new Configuration();
            string s = System.Windows.Forms.Application.StartupPath + "\\Setting.config";
            ConfigLoader.Load(s, config);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new StockService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}

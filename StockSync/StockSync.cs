using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;

//using System.Windows.Forms;

using StockCommon;
using HtmlAgilityPack;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockSync
{
    public class StockDataSync
    {
        public static DbUtility util;
        public static int ItemCount = 15; // 符合条件的单边股票数量
        
        public StockDataSync()
        {
            //StockCommon.LogManager.LogPath = System.AppDomain.CurrentDomain.BaseDirectory;

            InitDB();
        }

        public static void InitDB()
        {
//             if ( StockCommon.LogManager.LogPath == string.Empty)
//             {
//                 StockCommon.LogManager.LogPath = System.AppDomain.CurrentDomain.BaseDirectory;
//             }
            if (util == null)
            {
                util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            }
        }
        #region
        /// <summary>
        /// 同步所有股票信息
        /// 1.增加新股票
        /// 2.同步股票名称变动
        /// </summary>
        public static void SyncStockList()  
        {
            Configuration config = new Configuration();
            //string s = "server=localhost;uid=shenghai;pwd=123465;database=stock;"; /*System.Windows.Forms.Application.StartupPath + "\\StockService\\Setting.config";*/
            //string s = "E:\\Users\\shenghai\\Desktop\\Stock\\StockServiceUITest\\bin\\Debug\\StockService\\Setting.config";

            string strConfig = System.AppDomain.CurrentDomain.BaseDirectory + "Setting.config";
            ConfigLoader.Load(strConfig, config);
            LogManager.WriteLog(LogManager.LogFile.Trace, "Setting.config Path: " + strConfig);


            string tagUrl = Configuration.StockList;
            LogManager.WriteLog(LogManager.LogFile.Trace, "Configuration.StockList: " + tagUrl);

            CookieCollection cookies = new CookieCollection();
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();
            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();
            List<Stock> datas = new List<Stock>();//定义1个列表用于保存结果  
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(strWebData);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  
            HtmlNode nodes = htmlDocument.DocumentNode;
            HtmlNodeCollection collection = nodes.SelectNodes("//body/div/div/div/ul");
            foreach (HtmlNode node in collection)
            {
                //去除\r\n以及空格，获取到相应td里面的数据  
                string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in line)
                {
                    string tmp = item.Replace(")", "");
                    string[] st = tmp.Split('(');
                    if (StockLogic.GetCodeType(st[1]) != StockLogic.CodeType.UnKnown)   /*st[1].StartsWith("00") || st[1].StartsWith("6") || st[1].StartsWith("3")*/
                    {
                        datas.Add(new Stock() { StockName = st[0], StockCode = st[1] });
                    }
                }
            }

            SyncDatabase(ref datas);
        }

        private static void SyncDatabase(ref List<Stock> datas)
        {
            InitDB();

            foreach (Stock stock in datas)
            {
                string sql = string.Format("select * from STOCK where STOCKCODE={0}",stock.StockCode);
                DataTable table = util.ExecuteDataTable(sql, null);
                List<Stock> _table = EntityReader.GetEntities<Stock>(table);
                if (_table.Count < 1)
                {
                    string sqlInsert = string.Format("insert into STOCK values('{0}','{1}')", stock.StockName, stock.StockCode);
                    try
                    {
                        int reCount = util.ExecuteNonQuery(sqlInsert, null);
                    }
                    catch (System.Exception e)
                    {
                        LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncDatabase((ref List<Stock> datas))>, MSG : {0} ", e.Message));
                    }                   
                }
                else if (_table[0].StockName != stock.StockName)
                {
                    string sqlUpdate = string.Format("update STOCK set STOCKNAME='{0}' where STOCKCODE={1}", stock.StockName, stock.StockCode);
                    try
                    {
                        int reCount = util.ExecuteNonQuery(sqlUpdate, null);
                    }
                    catch (System.Exception e)
                    {
                        LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncDatabase((ref List<Stock> datas))>, MSG : {0} ", e.Message));
                    }  
                }
            }
        }
        #endregion

        #region
        /// <summary>
        /// 同步每天股票交易信息
        /// 新增每日交易信息
        /// </summary>
        public static void SyncDailyTradeData()
        {
            InitDB(); 
            string sql = string.Format("select * from STOCK");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stock> _table = EntityReader.GetEntities<Stock>(table);
            foreach (Stock stock in _table)
            {
                string jsUrl = StockLogic.GenerateStockItemJSUrl(stock.StockCode);
                CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(jsUrl, null, null, cookies);
                Stream stream = response.GetResponseStream();
                stream.ReadTimeout = 15 * 1000; //读取超时
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                string strWebData = sr.ReadToEnd();
                string[] lines = strWebData.Split('~');


            }
        }

        /// <summary>
        /// 根据js数据生成股票交易信息
        /// </summary>
        private static StockItem GenerateStockItem(ref string[] data)
        {
            StockItem item = new StockItem();
            item.StockCode = data[1];
            item.StockName = data[2];
            item.ClosePrice = Convert.ToDouble(data[3]);
            item.OpenPrice = Convert.ToDouble(data[5]);
            item.LowestPrice = Convert.ToDouble(data[43]);
            item.HighestPrice = Convert.ToDouble(data[42]);
            return item;
        }
        #endregion



        #region
        /// <summary>
        /// 同步股票交易信息
        /// 新增每日交易信息
        /// 同步3个月 之内所有股票日交易详细信息
        /// </summary>
        public static void SyncStockDataDetaileList(bool sync3month = true)
        {
            InitDB();    
            string sql = string.Format("select * from STOCK");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stock> _table = EntityReader.GetEntities<Stock>(table);
            foreach ( Stock stock in _table )
            {
                string resUrl = StockLogic.GenerateStockUrl(stock.StockCode, sync3month);
                string strHtml = GetHtmlString(resUrl);
                int pos = strHtml.IndexOf("\r\n");
                string strCsv = strHtml.Remove(0, pos);
                string _strCsv = Configuration.StockCSVHeader + strCsv;



                if (_strCsv.Contains("None"))
                {
                    _strCsv = _strCsv.Replace("None", "0.0");
                }
                


                TextReader rea = new StreamReader(StringToStream(_strCsv), System.Text.Encoding.UTF8);
                using (var reader = new CsvReader(rea))
                {
                    var records = reader.GetRecords<StockItem>();
                    int m = 0;
                    foreach (StockItem item in records)//var record
                    {
                        
                        string date = string.Format("{0}/{1}/{2}", item.StockDate.Year, item.StockDate.Month, item.StockDate.Day);
                        
                        /*{
                            if (m == 0 && date != "2014/9/24")
                            {
                                string cc = _strCsv;
                                int yy = 0;
                                yy++;
                            }
                        }
                        m++;*/
                        pos = item.StockCode.IndexOf("'");
                        string stockCode = item.StockCode.Remove(pos,1);  // 去除 " csv文件中股票代码前面的 '  如 '002560 "
                        item.StockCode = stockCode;

                        StockItem dbItem = GetStockItemFromDB(item);
                        if (dbItem != null)  // update
                        {
                            if (dbItem.Equals(item))
                            {
                                continue;
                            }
                            string sqlUpdate = string.Format("update STOCKITEM set STOCKCODE='{0}' , STOCKNAME='{1}' , OPENPRICE={2} , CLOSEPRICE={3} , HIGHESTPRICE={4} , LOWESTPRICE={5} , FLUCTUATEMOUNT={6} , FLUCTUATERATE={7} , CHANGERATE={8} , TRADEVOLUME={9} , TRADEMOUNT={10} , TOATLMARKETCAP={11} , CIRCULATIONMARKETCAP={12} where STOCKDATE='{13}'",
                                stockCode, item.StockName, item.OpenPrice, item.ClosePrice, item.HighestPrice, item.LowestPrice, item.FluctuateMount, item.FluctuateRate, item.ChangeRate, item.TradeVolume, item.TradeMount, item.ToatlMarketCap, item.CirculationMarketCap, date);
                            try
                            {
                                int reCount = util.ExecuteNonQuery(sqlUpdate, null);
                            }
                            catch (System.Exception e)
                            {
                                LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncStockDataDetaileList()>, MSG : {0} ", e.Message));
                            }
                        }
                        else // insert
                        {
                            string sqlInsert = string.Format("insert into STOCKITEM values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})",
                                date, stockCode, item.StockName, item.OpenPrice, item.ClosePrice, item.HighestPrice, item.LowestPrice, item.FluctuateMount, item.FluctuateRate, item.ChangeRate, item.TradeVolume, item.TradeMount, item.ToatlMarketCap, item.CirculationMarketCap);
                            try
                            {
                                int reCount = util.ExecuteNonQuery(sqlInsert, null);
                            }
                            catch (System.Exception e)
                            {
                                LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <SyncStockDataDetaileList()>, MSG : {0} ", e.Message));
                            }
                        }
                    }
                }

            }
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        public static string GetHtmlString(string url)
        {
            CookieCollection cookies = new CookieCollection(); 
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, null, cookies);
            Stream stream = response.GetResponseStream();
            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();
            return strWebData;
        }

        public static StockItem GetStockItemFromDB(StockItem item)
        {
            InitDB();
            DateTime datetime = item.StockDate;
            string date = string.Format("{0}/{1}/{2}", datetime.Year, datetime.Month, datetime.Day);
            string sql = string.Format("select * from STOCKITEM where STOCKDATE='{0}' and STOCKCODE='{1}'", date, item.StockCode);
            StockItem _item = util.QueryForObject<StockItem>(sql, null);
            return _item;
        }
        #endregion

        #region
        /// <summary>
        /// 同步常规方法更新不到的股票日交易数据
        /// </summary>
        public static void SyncStockDataDetaileListExt()
        {
            InitDB();
            string recentday = TransactionDate.GetRecentDay();
            string sql = string.Format("SELECT * FROM STOCK WHERE STOCKCODE NOT IN (SELECT STOCKCODE FROM  STOCKITEM WHERE STOCKDATE='{0}')", recentday);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stock> _table = EntityReader.GetEntities<Stock>(table);
            foreach (Stock stock in _table)
            {
                string resUrl = StockLogic.GenetateStockUrlEx(stock.StockCode);

                CookieCollection cookies = new CookieCollection();
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(resUrl, null, null, cookies);
                Stream stream = response.GetResponseStream();
                stream.ReadTimeout = 15 * 1000; //读取超时
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                string strWebData = sr.ReadToEnd();
                strWebData = strWebData.Replace("_ntes_quote_callback(", "");
                strWebData = strWebData.Replace(");", "");
                Newtonsoft.Json.Linq.JObject obj = JsonConvert.DeserializeObject(strWebData, typeof(Newtonsoft.Json.Linq.JObject)) as Newtonsoft.Json.Linq.JObject;
                if (obj == null)
                {
                    continue;
                }
                List<StockItem> items = new List<StockItem>();
                StockItem item = new StockItem();
                item.StockCode = stock.StockCode;
                item.StockName = stock.StockName;

                JToken token = obj.First.First;
                double openprice = Convert.ToDouble(token["open"].ToString());
                double closeprice = Convert.ToDouble(token["price"].ToString());
                double highestprice = Convert.ToDouble(token["high"].ToString());
                double lowestprice = Convert.ToDouble(token["low"].ToString());
                double fluctuatemount = Convert.ToDouble(token["updown"].ToString());
                double fluctuaterate = Convert.ToDouble(token["percent"].ToString())*100;

                items.Add(item);
                FillStockItemTable("STOCKITEM", ref items);
            }
        }
        #endregion

        /// <summary>
        /// 计算股票边界值 保存到数据库
        /// 在更新当日交易数据之后  计算接口开始执行
        /// 执行完毕写入更新日期
        /// </summary>
        public static void ComputeStockSide()
        {
           // GetLastClosePrice("300389");
           // SyncRecentDaysFluctuateRate(15);


            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, string.Format("path {0}", StockCommon.LogManager.LogPath));
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "ComputeStockSide start");
            SyncTodayFluctuateRate();
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncTodayFluctuateRate");

            SyncTodayChangeRate();
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncTodayChangeRate");


            SyncRecentDaysChangeRate(2);
            SyncRecentDaysFluctuateRate(2);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 2");

            SyncRecentDaysChangeRate(3);
            SyncRecentDaysFluctuateRate(3);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 3");

            SyncRecentDaysChangeRate(5);
            SyncRecentDaysFluctuateRate(5);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 5");

            SyncRecentDaysChangeRate(10);
            SyncRecentDaysFluctuateRate(10);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 10");

            SyncRecentDaysChangeRate(15);
            SyncRecentDaysFluctuateRate(15);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 15");

            SyncRecentDaysChangeRate(30);
            SyncRecentDaysFluctuateRate(30);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 30");

            SyncRecentDaysChangeRate(45);
            SyncRecentDaysFluctuateRate(45);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 45");

            SyncRecentDaysChangeRate(60);
            SyncRecentDaysFluctuateRate(60);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "SyncRecentDays 60");

            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "ComputeStockSide end");
        }

        #region 
        /// <summary>
        /// 计算股票当天涨跌幅 
        /// </summary>
        private static void SyncTodayFluctuateRate()
        {
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "计算股票当天涨跌幅 ");
            InitDB();//
            string recentday = TransactionDate.GetRecentDay();
            string tableName = "STOCKITEM_DAILYFLUCTUATERATE";
            string sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE='{0}' ORDER BY FLUCTUATERATE DESC LIMIT 0,{1}", recentday, ItemCount);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> _table = EntityReader.GetEntities<StockItem>(table);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, string.Format("涨幅最大 _Table.count{0} ", _table.Count));

            string _sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE='{0}' ORDER BY FLUCTUATERATE ASC LIMIT 0,{1}", recentday, ItemCount);
            DataTable table_E = util.ExecuteDataTable(_sql, null);
            List<StockItem> _table_E = EntityReader.GetEntities<StockItem>(table_E);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, string.Format("跌幅最大 _Table_E.count{0} ", _table_E.Count));

            if (!IsStockItemTableEmpty(tableName))
            {
                EmptyStockItemTable(tableName);
            }
            FillStockItemTable(tableName, ref _table);
            FillStockItemTable(tableName, ref _table_E);
            StockCommon.LogManager.WriteLog(StockCommon.LogManager.LogFile.Trace, "计算股票当天涨跌幅 结束");
        }

        /// <summary>
        /// 计算股票当天换手率  
        /// 换手率只计算最大值
        /// </summary>
        private static void SyncTodayChangeRate()
        {
            InitDB();
            string recentday = TransactionDate.GetRecentDay();
            string tableName = "STOCKITEM_DAILYCHANGERATE";
            string sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE='{0}' ORDER BY CHANGERATE DESC LIMIT 0,{1}", recentday, ItemCount * 2);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> _table = EntityReader.GetEntities<StockItem>(table);

            //string _sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE='2014/8/8' and CHANGERATE!=0 ORDER BY CHANGERATE ASC LIMIT 0,{0}", ItemCount);
            //DataTable table_E = util.ExecuteDataTable(_sql, null);
            //List<StockItem> _table_E = EntityReader.GetEntities<StockItem>(table_E);

            if (!IsStockItemTableEmpty(tableName))
            {
                EmptyStockItemTable(tableName);
            }
            FillStockItemTable(tableName, ref _table);
            //FillStockItemTable(tableName, ref _table_E);
        }
        #endregion

        #region
        /// <summary>
        /// 计算股票**个交易日换手率  
        /// </summary>
        private static void SyncRecentDaysChangeRate(int days)
        {
            InitDB();
            bool IsChangerateMain = true;
            string tableName = "STOCKITEM_CHANGERATE_FLUCTUATERATE";
            string recentDay = TransactionDate.GetRecentDay();
            string destDay = TransactionDate.GetDatesOfTransaction(days);
            string sql = string.Format("SELECT B.STOCKCODE, B.STOCKNAME,(SELECT S.CLOSEPRICE FROM STOCKITEM S WHERE S.STOCKDATE='{0}' AND S.STOCKCODE=B.STOCKCODE) AS CLOSEPRICE,SUM(CHANGERATE) AS CHANGERATE, SUM(FLUCTUATERATE) AS FLUCTUATERATE FROM STOCKITEM B WHERE B.STOCKDATE >='{1}' GROUP BY B.STOCKCODE ORDER BY CHANGERATE DESC LIMIT 0,{2}", recentDay, destDay, ItemCount * 2);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<TEMP_Stockitem_Changerate_Fluctuaterate> _table = EntityReader.GetEntities<TEMP_Stockitem_Changerate_Fluctuaterate>(table);

            List<Stockitem_Changerate_Fluctuaterate> UpStockitems = new List<Stockitem_Changerate_Fluctuaterate>();
            foreach (TEMP_Stockitem_Changerate_Fluctuaterate item in _table)
            {
                Stockitem_Changerate_Fluctuaterate _item = new Stockitem_Changerate_Fluctuaterate(item);
                if (_item.ClosePrice == 0)
                {
                    _item.ClosePrice = GetLastClosePrice(_item.StockCode);
                }
                _item.ChangerateMain = IsChangerateMain;
                _item.TradeDays = days;
                UpStockitems.Add(_item);
            }
            if (!IsStockitem_Changerate_FluctuaterateTableEmpty(tableName, IsChangerateMain, days))
            {
                EmptyStockitem_Changerate_FluctuaterateTable(tableName, IsChangerateMain, days);
            }
            FillStockitem_Changerate_FluctuaterateTable(tableName, ref UpStockitems, IsChangerateMain, days);
        }

        /// <summary>
        /// 计算股票**个交易日涨跌幅 
        /// </summary>
        private static void SyncRecentDaysFluctuateRate(int days)
        {
            InitDB();
            bool IsChangerateMain = false;
            string tableName = "STOCKITEM_CHANGERATE_FLUCTUATERATE";
            string recentDay = TransactionDate.GetRecentDay();
            string destDay = TransactionDate.GetDatesOfTransaction(days);
            string sql = string.Format("SELECT B.STOCKCODE, B.STOCKNAME,(SELECT S.CLOSEPRICE FROM STOCKITEM S WHERE S.STOCKDATE='{0}' AND S.STOCKCODE=B.STOCKCODE) AS CLOSEPRICE,SUM(CHANGERATE) AS CHANGERATE, SUM(FLUCTUATERATE) AS FLUCTUATERATE FROM STOCKITEM B WHERE B.STOCKDATE >='{1}' GROUP BY B.STOCKCODE ORDER BY FLUCTUATERATE DESC LIMIT 0,{2}", recentDay, destDay, ItemCount);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<TEMP_Stockitem_Changerate_Fluctuaterate> _table = EntityReader.GetEntities<TEMP_Stockitem_Changerate_Fluctuaterate>(table);

            List<Stockitem_Changerate_Fluctuaterate> UpStockitems = new List<Stockitem_Changerate_Fluctuaterate>();
            foreach(TEMP_Stockitem_Changerate_Fluctuaterate item in _table)
            {
                Stockitem_Changerate_Fluctuaterate _item = new Stockitem_Changerate_Fluctuaterate(item);
                if (_item.ClosePrice == 0)
                {
                    _item.ClosePrice = GetLastClosePrice(_item.StockCode);
                }
                _item.ChangerateMain = IsChangerateMain;
                _item.TradeDays = days;
                UpStockitems.Add(_item);
            }

            string _sql = string.Format("SELECT B.STOCKCODE, B.STOCKNAME,(SELECT S.CLOSEPRICE FROM STOCKITEM S WHERE S.STOCKDATE='{0}' AND S.STOCKCODE=B.STOCKCODE) AS CLOSEPRICE,SUM(CHANGERATE) AS CHANGERATE, SUM(FLUCTUATERATE) AS FLUCTUATERATE FROM STOCKITEM B WHERE B.STOCKDATE >='{1}' GROUP BY B.STOCKCODE ORDER BY FLUCTUATERATE ASC LIMIT 0,{2}", recentDay, destDay, ItemCount);
            DataTable table_E = util.ExecuteDataTable(_sql, null);
            List<TEMP_Stockitem_Changerate_Fluctuaterate> _table_E = EntityReader.GetEntities<TEMP_Stockitem_Changerate_Fluctuaterate>(table_E);

            List<Stockitem_Changerate_Fluctuaterate> DownStockitems = new List<Stockitem_Changerate_Fluctuaterate>();
            foreach (TEMP_Stockitem_Changerate_Fluctuaterate item in _table_E)
            {
                Stockitem_Changerate_Fluctuaterate _item = new Stockitem_Changerate_Fluctuaterate(item);
                if (_item.ClosePrice == 0)
                {
                    _item.ClosePrice = GetLastClosePrice(_item.StockCode);
                }
                _item.ChangerateMain = IsChangerateMain;
                _item.TradeDays = days;
                DownStockitems.Add(_item);
            }

            if (!IsStockitem_Changerate_FluctuaterateTableEmpty(tableName, IsChangerateMain, days))
            {
                EmptyStockitem_Changerate_FluctuaterateTable(tableName, IsChangerateMain, days);
            }
            FillStockitem_Changerate_FluctuaterateTable(tableName, ref UpStockitems, IsChangerateMain, days);
            FillStockitem_Changerate_FluctuaterateTable(tableName, ref DownStockitems, IsChangerateMain, days);        
        }
        #endregion

        #region
        private static bool IsStockItemTableEmpty(string tableName)
        {
            bool ISEmpty = false;
            InitDB();
            string sql = string.Format("SELECT * FROM {0}", tableName);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> values = EntityReader.GetEntities<StockItem>(table);
            if (values.Count == 0)
            {
                ISEmpty = true;
            }
            return ISEmpty;
        }

        private static bool IsStockitem_Changerate_FluctuaterateTableEmpty(string tableName, bool IsChangerateMain, int Days)
        {
            bool ISEmpty = false;
            InitDB();
            string sql = string.Format("SELECT * FROM {0} WHERE CHANGERATEMAIN={1} AND TRADEDAYS={2}", tableName, IsChangerateMain, Days);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stockitem_Changerate_Fluctuaterate> values = EntityReader.GetEntities<Stockitem_Changerate_Fluctuaterate>(table);
            if (values.Count == 0)
            {
                ISEmpty = true;
            }
            return ISEmpty;
        }

        private static void EmptyStockItemTable(string tableName)
        {
            string sqlDelete = string.Format("DELETE FROM {0}", tableName);
            try
            {
                util.ExecuteNonQuery(sqlDelete, null);
            }
            catch (Exception e)
            {
                LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <EmptyStockItemTable(string tableName)>, MSG : {0} ", e.Message));
            }
        }

        private static void EmptyStockitem_Changerate_FluctuaterateTable(string tableName, bool IsChangerateMain, int Days)
        {
            string sqlDelete = string.Format("DELETE FROM {0} WHERE CHANGERATEMAIN={1} AND TRADEDAYS={2}", tableName, IsChangerateMain, Days);
            try
            {
                util.ExecuteNonQuery(sqlDelete, null);
            }
            catch (Exception e)
            {
                LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <EmptyStockitem_Changerate_FluctuaterateTable(string tableName, bool IsChangerateMain, int Days)>, MSG : {0} ", e.Message));
            }
        }

        private static void FillStockitem_Changerate_FluctuaterateTable(string tableName, ref List<Stockitem_Changerate_Fluctuaterate> items, bool IsChangerateMain, int Days)
        {
            foreach (Stockitem_Changerate_Fluctuaterate item in items)
            {
                string sqlInsert = string.Format("insert into {0} values('{1}','{2}','{3}',{4},{5},{6},{7})",
                                    tableName, item.StockCode, item.StockName, item.ClosePrice, item.FluctuateRate, item.ChangeRate, IsChangerateMain, Days);
                try
                {
                    int reCount = util.ExecuteNonQuery(sqlInsert, null);
                }
                catch (System.Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <FillStockitem_Changerate_FluctuaterateTable(string tableName, ref List<Stockitem_Changerate_Fluctuaterate> items, bool IsChangerateMain, int Days)>, MSG : {0} ", e.Message));
                }

            }
        }

        private static void FillStockItemTable(string tableName, ref List<StockItem> items)
        {
            foreach (StockItem item in items)
            {
                string date = string.Format("{0}/{1}/{2}", item.StockDate.Year, item.StockDate.Month, item.StockDate.Day);
                string sqlInsert = string.Format("insert into {0} values('{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})",
                                    tableName, date, item.StockCode, item.StockName, item.OpenPrice, item.ClosePrice, item.HighestPrice, item.LowestPrice, item.FluctuateMount, item.FluctuateRate, item.ChangeRate, item.TradeVolume, item.TradeMount, item.ToatlMarketCap, item.CirculationMarketCap);
                try
                {
                    int reCount = util.ExecuteNonQuery(sqlInsert, null);
                }
                catch (System.Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : <FillStockItemTable(string tableName, ref List<StockItem> items)>, MSG : {0} ", e.Message));
                }

            }
        }
        #endregion

        #region
        /// <summary>
        /// 同步最后更新时间
        /// </summary>
        public static void SyncLastUpdate()
        {
            InitDB();
            string sql = string.Format("SELECT * FROM LASTUPDATE");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<LastUpdate> values = EntityReader.GetEntities<LastUpdate>(table);
            if (values.Count == 0)  // insert 
            {
                DateTime today = DateTime.Now;
                string date = string.Format("{0}/{1}/{2}", today.Year, today.Month, today.Day);
                string sqlInsert = string.Format("INSERT INTO LASTUPDATE VALUES('{0}')", date);
                try
                {
                    util.ExecuteNonQuery(sqlInsert, null);
                }
                catch(Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < SyncLastUpdate()>, MSG : {0} ", e.Message));
                }
            }
            else if (values.Count > 1)  // delete and insert
            {
                string sqlDelete = string.Format("DELETE FROM LASTUPDATE");
                try
                {
                    util.ExecuteNonQuery(sqlDelete, null);
                }
                catch (Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < SyncLastUpdate()>, MSG : {0} ", e.Message));
                }

                DateTime today = DateTime.Now;
                string date = string.Format("{0}/{1}/{2}", today.Year, today.Month, today.Day);
                string sqlInsert = string.Format("INSERT INTO LASTUPDATE VALUES('{0}')", date);
                try
                {
                    util.ExecuteNonQuery(sqlInsert, null);
                }
                catch (Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < SyncLastUpdate()>, MSG : {0} ", e.Message));
                }
            }
            else // update
            {
                DateTime today = DateTime.Now;
                string date = string.Format("{0}/{1}/{2}", today.Year, today.Month, today.Day);
                string sqlUpdate = string.Format("update LASTUPDATE set LASTUPDATETIME='{0}'", date);
                try
                {
                    util.ExecuteNonQuery(sqlUpdate, null);
                }
                catch (Exception e)
                {
                    LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < SyncLastUpdate()>, MSG : {0} ", e.Message));
                }
            }
        }
        #endregion

        #region
        /// <summary>
        /// 同步股票交易日期（加入所有交易日日期）
        /// </summary>
        public static void SyncTradeAllDate()
        {
            TransactionDate.SyncTradeAllDate();
        }

        /// <summary>
        /// 同步股票交易日期（加入当日交易日日期） 
        /// </summary>
        public static void SyncTradeCurrentDate()
        {
            TransactionDate.SyncTradeCurrentDate();
        }
        #endregion

        #region
        /// <summary>
        /// 获得股票最后一个交易日收盘价
        /// 对应情况  最近几个交易日停牌  得到的收盘价是0
        /// </summary>
        private static double GetLastClosePrice(string stockcode)
        {
            double closeprice = 0;
            InitDB();
            string recentDay = TransactionDate.GetRecentDay();
            string destDay = TransactionDate.GetDatesOfTransaction(200);
            string sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE > '{0}' AND STOCKDATE <= '{1}' AND STOCKCODE='{2}' ORDER BY STOCKDATE DESC", destDay, recentDay, stockcode);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> _table = EntityReader.GetEntities<StockItem>(table);
            foreach( StockItem item in _table)
            {
                if (item.ClosePrice !=0)
                {
                    closeprice = item.ClosePrice;
                    break;
                }
            }
            return closeprice;
        }
        #endregion

        public static void test()
        {
            //TransactionDate.SyncTradeAllDate();
            SyncRecentDaysFluctuateRate(2);
            //TransactionDate.GetDatesOfTransaction(420);
            //SyncRecentDaysFluctuateRate(2);

            SyncRecentDaysChangeRate(3);
        }
    }
}

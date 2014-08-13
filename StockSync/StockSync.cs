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

namespace StockSync
{
    public class StockDataSync
    {
        public static DbUtility util;
        public static int ItemCount = 15; // 符合条件的单边股票数量

        public StockDataSync()
        {
            InitDB();
        }

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

                    }  
                }
            }
        }

        /// <summary>
        /// 同步股票交易信息
        /// 新增每日交易信息
        /// 同步3个月 之内所有股票日交易详细信息
        /// </summary>
        public static void SyncStockDataDetaileList()
        {
            InitDB();    
            string sql = string.Format("select * from STOCK");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stock> _table = EntityReader.GetEntities<Stock>(table);
            foreach ( Stock stock in _table )
            {
                string resUrl = StockLogic.GenetateStockUrl(stock.StockCode, true);
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

                    foreach (StockItem item in records)//var record
                    {
                        string date = string.Format("{0}/{1}/{2}", item.StockDate.Year, item.StockDate.Month, item.StockDate.Day);
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

        public static void InitDB()
        {
            if (util == null)
            {
                util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            }
        }

        /// <summary>
        /// 计算股票边界值 保存到数据库
        /// 在更新当日交易数据之后  计算接口开始执行
        /// 执行完毕写入更新日期
        /// </summary>
        public  static void ComputeStockSide()
        {
            ComputeTodayFluctuateRate();
        }

        /// <summary>
        /// 计算股票当天涨跌幅
        /// </summary>
        public static void ComputeTodayFluctuateRate()
        {
            InitDB();
            string sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE='2014/8/8' ORDER BY FLUCTUATERATE DESC LIMIT 0,{0}", ItemCount);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> _table = EntityReader.GetEntities<StockItem>(table);

            string _sql = string.Format("SELECT * FROM STOCKITEM WHERE STOCKDATE='2014/8/8' ORDER BY FLUCTUATERATE ASC LIMIT 0,{0}", ItemCount);
            DataTable table_E = util.ExecuteDataTable(_sql, null);
            List<StockItem> _table_E = EntityReader.GetEntities<StockItem>(table_E);
        }

    }
}

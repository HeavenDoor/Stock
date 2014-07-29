using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;
using MySql.Data.MySqlClient;

using System.ServiceProcess;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using StockCommon;
using HtmlAgilityPack;

namespace StockServiceUITest.TestCode
{
    public class LogManagerTest
    {
        public static void testLogManager()
        {
            LogManager.LogPath = "E://";
            LogManager.WriteLog(LogManager.LogFile.Trace, "shenghai");
        }
    }

    public class ServiceProcessTest
    {
        public static void testServe()
        {
            ServiceController controller = new ServiceController("StockService");
            controller.Stop();
        }
    }

    public class HttpTest
    {
        public static void testHttpGet()
        {
            string userName = "15528358573";
            string tagUrl = Configuration.StockList;//"http://www.sina.com/";
            CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();

            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();
        }

        public static void testHttpPost()
        {
            string loginUrl = "https://passport.baidu.com/?login";
            string userName = "15528358573";
            string password = "123465";
            string tagUrl = "http://cang.baidu.com/" + userName + "/tags";
            Encoding encoding = Encoding.GetEncoding("gb2312");

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("tpl", "fa");
            parameters.Add("tpl_reg", "fa");
            parameters.Add("u", tagUrl);
            parameters.Add("psp_tt", "0");
            parameters.Add("username", userName);
            parameters.Add("password", password);
            parameters.Add("mem_pass", "1");
            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            string cookieString = response.Headers["Set-Cookie"];
        }
    }

    public class DbUtilityTest
    {
        public static void testSql()
        {
            string conStr = "server=localhost;uid=shenghai;" + "pwd=123465;database=stock;";
            DbUtility util = new DbUtility(conStr, DbProviderType.MySql);
            DbDataReader reader = util.ExecuteReader("SELECT * FROM stockcode", null);
            DataTable table = util.ExecuteDataTable("SELECT * FROM stockcode", null);
            List<stock> _reader = EntityReader.GetEntities<stock>(reader);
            List<stock> _table = EntityReader.GetEntities<stock>(table);
        }
    }

    public class ConfigTest
    {
        public static void testConfig()
        {
            var config = new Configuration();
            string s = System.Windows.Forms.Application.StartupPath + "\\StockService\\Setting.config";
            ConfigLoader.Load(s, config);
        }
    }

    public class MyLambda
    {
        public void disPlay()
        {
            string mid = ",middle part,";
            Func<string, string> lambda = param =>
            {
                param += mid; param += "and this was added to the string";
                return param;
            }; 
            Console.WriteLine(lambda("Start of string"));
        }
    } 
    public class Test
    {
        public static void test()
        {
            string strWebContent = @"<table><thead>  
    <tr>  
      <th>时间</th>  
      <th>类型</th>  
      <th>名称</th>  
      <th>单位</th>  
      <th>金额</th>  
    </tr>  
    </thead>  
    <tbody>" +
    @"<tr>  
      <td>2013-12-29</td>  
      <td>发票1</td>  
      <td>采购物资发票1</td>  
      <td>某某公司1</td>  
      <td>123元</td>  
    </tr>" +
    @"<tr>  
      <td>2013-12-29</td>  
      <td>发票2</td>  
      <td>采购物资发票2</td>  
      <td>某某公司2</td>  
      <td>321元</td>  
    </tr>  
    </tbody>  
  </table>  
";


            List<Data> datas = new List<Data>();//定义1个列表用于保存结果  

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(strWebContent);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  

            HtmlNodeCollection collection = htmlDocument.DocumentNode.SelectSingleNode("table/tbody").ChildNodes;//跟Xpath一样，轻松的定位到相应节点下  
            foreach (HtmlNode node in collection)
            {
                //去除\r\n以及空格，获取到相应td里面的数据  
                string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                //如果符合条件，就加载到对象列表里面  
                if (line.Length == 5)
                    datas.Add(new Data() { 时间 = line[0], 类型 = line[1], 名称 = line[2], 单位 = line[3], 金额 = line[4] });
            }

            //循环输出查看结果是否正确  
            foreach (var v in datas)
            {
                Console.WriteLine(string.Join(",", v.时间, v.类型, v.名称, v.单位, v.金额));
            }  

        }


        public static void testlist()
        {
            string tagUrl = Configuration.StockList;//"http://www.sina.com/";
            CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();

            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();




            List<StockListTmp> datas = new List<StockListTmp>();//定义1个列表用于保存结果  

            HtmlDocument htmlDocument = new HtmlDocument();
            
            htmlDocument.LoadHtml(strWebData);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  

            HtmlNode nodes = htmlDocument.DocumentNode;
            HtmlNodeCollection collection = nodes.SelectNodes("//body/div/div/div/ul");
            foreach (HtmlNode node in collection)
            {
                //去除\r\n以及空格，获取到相应td里面的数据  
                string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach(string item in line)
                {
                    item.Replace(")", "");
                    string[] s = item.Split('(');
                    if (s[1].StartsWith("00") || s[1].StartsWith("6") || s[1].StartsWith("3"))
                    {
                        datas.Add(new StockListTmp() { name = s[0], code = s[1]});
                    }
                }
                
            }
        }
    }

    public class StockListTmp
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Data
    {
        public string 时间 { get; set; }
        public string 类型 { get; set; }
        public string 名称 { get; set; }
        public string 单位 { get; set; }
        public string 金额 { get; set; }
    } 
}

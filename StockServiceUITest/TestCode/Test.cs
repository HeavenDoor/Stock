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
            string tagUrl = "http://www.sina.com/";// +userName + "/tags";
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


}

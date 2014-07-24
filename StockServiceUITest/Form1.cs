using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
//using System.DirectoryServices.Protocols;
//using System.ServiceModel.Security;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

using System.Data.SqlClient;
using System.Data.Sql;
using MySql.Data.MySqlClient;

namespace StockServiceUITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------------------------------
            //ServiceController controller = new ServiceController("StockService");
            //controller.Stop();

            //-----------------------------------------------------------------------------

           /* string loginUrl = "https://passport.baidu.com/?login";
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
            string cookieString = response.Headers["Set-Cookie"];*/
            //---------------------------------------------------------------------------------

            /*string userName = "15528358573";
            string tagUrl = "http://www.sina.com/";// +userName + "/tags";
            CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();
           
            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();*/

            //-----------------------------------------------------------------------------------------------

            /*MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.ConnectionString = "server=localhost;uid=shenghai;" + "pwd=123465;database=stock;";
            conn.Open();
            cmd.Connection = conn;
            DataSet dataSet1 = new DataSet();
            MySql.Data.MySqlClient.MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SELECT * FROM stockcode;", conn);//读数据库  
            cmd.CommandText = "SELECT * FROM stockcode";
            dataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataAdapter.FillSchema(dataSet1, SchemaType.Source, "identify");
            dataAdapter.Fill(dataSet1, "IDENTIFY");//填充DataSet控件  
            dataGridView1.DataSource = dataSet1.Tables["identify"];//注意，DataSet中的数据表依次为Table, Table1, Table2...  
            dataGridView1.Update();*/
            //----------------------------------------------------------------------------------------------
        }
    }
}

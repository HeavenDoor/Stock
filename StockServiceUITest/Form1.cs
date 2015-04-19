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


using System.Data.Common;
using MySql.Data.MySqlClient;
using StockCommon;
using StockServiceUITest.TestCode;
using StockSync;

//using Quartz;

namespace StockServiceUITest
{
    public partial class Form1 : Form
    {
        public DrawValidationCode image;
        public Form1()
        {
            var config = new Configuration();
            string s = System.Windows.Forms.Application.StartupPath + "\\StockService\\Setting.config";
            ConfigLoader.Load(s, config);

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------
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
            // All Test
            //---------------------------------------------------------------------------------------
            //LogManagerTest.testLogManager();

            HttpTest.testHttpPost();

            {
                string dateString = "20150316104908";
                DateTime dt = DateTime.ParseExact(dateString, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                double v = 10;
                double.TryParse("sss", out v);
            }

            //Test.SyncStockList();  // 同步股票列表
            //SyncTest.SyncStockDataDetaileList();
           // SyncTest.SyncDailyTradeData();


            SyncTest.ComputeStockSide();
            //HttpTest.testHttpGet("http://qt.gtimg.cn/q=sh600027");
            //DbUtilityTest.testSql();
            //ServiceProcessTest.testServe();
            //-------------------------------------------------------------------------------------------

            //ConfigTest.testConfig();
            //string ss = Configuration.StockList;

           // HttpTest.testHttpGet("123");
            //------------------------------------------------------------------------------------

            //Test.testlist();

            
            //Test.cc();
            //Test.kk();
            // StockSync.StockDataSync.SyncStockList();
// 
//             DateTime currenttime = System.DateTime.Now;
//             if (currenttime.Hour == 16 && currenttime.Minute == 5 /*&& currenttime.Second == 0*/)
//             {
//                 int m = 0;
//             }

//             DateTime dt = DateTime.Now;
//             string day = dt.DayOfWeek.ToString();

  
            //Test.testEqual();

            //SyncTest.SyncLastUpdate();

            //SyncTest.ComputeStockSide();

            //SyncTest.SyncStockDataDetaileList();

            //SyncTest.testQuery();

            //CSVTest.ReadAllRecords();

            //SyncTest.SyncTradeDate();

            //SyncTest.test();

            //SyncTest.SyncStockDataDetaileListExt();

            string yy = "2014/03/05";
            string strToday = yy.Replace("/", "");

            string recentday = weekTest.GetRecentDay();
            string time = DateTime.Now.ToString();
            image = new DrawValidationCode();

            DateTime today = DateTime.Now;
            DateTime pre = DateTime.Now.AddDays(-1000);
            byte[] byteArray = new byte[image.Width*image.Height];
            Stream stream = new MemoryStream(byteArray);


            //FileStream fs = new FileStream(@"D:\aab.png", FileMode.CreateNew); 
            image.CreateImage(stream);
            string kk = image.ValidationCode;
            int m = kk.Length;
            char a = kk[0];
            char b = kk[1];
            char c = kk[2];
            char d = kk[3];

            pictureBox1.Image = Image.FromStream(stream);
            label1.Text = kk;
        }

        public void test()
        {

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    public class stock
    {
        public stock(){}
        private string name;
        public string StockName
        {
            get {return name;}
            set { name = value; }
        }

        private string code;
        public string StockCode
        {
            get { return code; }
            set { code = value; }
        }
    }
}

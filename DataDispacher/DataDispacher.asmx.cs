using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Common;

using StockCommon;
using DataDispacher.Logic;

namespace DataDispacher
{
    /// <summary>
    /// Summary description for DataDispacher
    /// </summary>
    /// 
    
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataDispacher : System.Web.Services.WebService
    {
        private static string connectString = "server=localhost;uid=shenghai;pwd=123465;database=stock;";
        private DbUtility util = new DbUtility(delegate (string str) { return new string("123");}, DbProviderType.MySql);
        private Configuration config = new Configuration();
        

        #region [WebMethod] TestConnection
        [WebMethod]
        public bool TestConnection()
        {
            //ConfigLoader.Load(System.Windows.Forms.Application.StartupPath + "\\Setting.config", config);
            return true;
        }
        #endregion

        #region [WebMethod] RegisterUser
        [WebMethod]
        public string RegisterUser(string userName, string pwd, string email, string phone)
        {
            //DbUtility uu = new DbUtility(delegate(string str) { return "ss"; }, DbProviderType.MySql);

            List<string> list=new List<string>();
            var numbers = new []{ "5", "4", "1", "3", "9", "8", "6", "7", "2", "0" };
            list.AddRange(numbers);
            list.Sort(delegate (string a, string b)
                {
                    ConfigLoader.Load("",config);
                    /*return*/ a.CompareTo(b);
                    ConfigLoader.Load("", config);
                    b = a =  Configuration.SqlConnectStr;
                    return a/*.CompareTo(b)*/;
                }
            );

           // list.Sort((a,b)=>a.CompareTo(b));




            if (!TestConnection())
            {
                return "";
            }

            LogicBase response = new LogicBase();
            string identfySql = string.Format("select * from user where username='{0}'", userName);
            int userCounts = EntityReader.GetEntities<User>(util.ExecuteDataTable(identfySql, null)).Count;
            if (userCounts > 0)
            {
                response.ErrorType = (int)Logic.ErrorType.UserExists;
                response.ErrorID = (int)Logic.ErrorID.UserExists;
                response.ReturnMessage = "User has bin exists";
                string s = SerializationHelper<LogicBase>.Serialize(response);
                return s;
            }

            string sql = string.Format("insert into user values({0},{1},{2},{3})",userName, pwd,email,phone);
            int value = util.ExecuteNonQuery(sql,null);
            return "";
        }
        #endregion


        private void InitConnection()
        {

        }

    }
}

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
        private static string connectString = "server=localhost;uid=shenghai;" + "pwd=123465;database=stock;";
        private DbUtility util = new DbUtility(connectString, DbProviderType.MySql);
        
        #region [WebMethod] TestConnection
        [WebMethod]
        public string TestConnection()
        {
            return "shenghai";
        }
        #endregion

        #region [WebMethod] RegisterUser
        [WebMethod]
        public string RegisterUser(string userName, string pwd, string email, string phone)
        {
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

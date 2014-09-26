using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;

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
        private DbUtility util;
        private Configuration config;

        public DataDispacher()  
        {
            StockCommon.LogManager.LogPath = System.AppDomain.CurrentDomain.BaseDirectory;
            config = new Configuration();
            string strConfig = System.AppDomain.CurrentDomain.BaseDirectory + "Setting.config";
            ConfigLoader.Load(strConfig, config);
            util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
        }
        #region [WebMethod] Login
        [WebMethod]
        public string Login(string email, string passWord)
        {
            bool vergify = VerifyUser(email, passWord);
            string result = string.Empty;
            LogicBase response = new LogicBase();
            if (!vergify)
            {
                response.ErrorType = (int)Logic.ErrorType.UserNameOrPWDError;
                response.ErrorID = (int)Logic.ErrorID.ValidateUserFailure;
                response.ReturnMessage = "用户名或密码错误";
                result = SerializationHelper<LogicBase>.Serialize(response);
                return result;
            }
            response.ErrorType = (int)Logic.ErrorType.NoException;
            response.ErrorID = (int)Logic.ErrorID.NoError;
            response.ReturnMessage = "success";
            result = SerializationHelper<LogicBase>.Serialize(response);
            return result;
        }
        #endregion

        #region [WebMethod] TestConnection
        [WebMethod]
        public bool TestConnection()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            //ConfigLoader.Load(System.Windows.Forms.Application.StartupPath + "\\Setting.config", config);
            return true;
        }
        #endregion

        #region [WebMethod] GetLastUpdate
        [WebMethod]
        public string GetLastUpdate(string email,  string passWord)
        {
            bool vergify = VerifyUser(email, passWord);
            string result = string.Empty;
            LogicBase response = new LogicBase();
            if (!vergify)
            {
                response.ErrorType = (int)Logic.ErrorType.UserNameOrPWDError;
                response.ErrorID = (int)Logic.ErrorID.ValidateUserFailure;
                response.ReturnMessage = "用户名或密码错误";
                result = SerializationHelper<LogicBase>.Serialize(response);
                return result;
            }
            string sql = string.Format("SELECT * FROM LASTUPDATE");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<LastUpdate> _table = EntityReader.GetEntities<LastUpdate>(table);

            string dt = _table[0].LASTUPDATETIME;
            DateTime dateEx = Convert.ToDateTime(dt);
            string date = string.Format("{0}/{1}/{2}", dateEx.Year, dateEx.Month, dateEx.Day);
            
            response.ErrorType = (int)Logic.ErrorType.NoException;
            response.ErrorID = (int)Logic.ErrorID.NoError;
            response.ReturnMessage = date;
            result = SerializationHelper<LogicBase>.Serialize(response);
            return result;
        }
        #endregion

        #region [WebMethod] RegisterUser
        [WebMethod]
        public string RegisterUser(string email, string userName, string pwd, string phone, string phoneID, string validationCode)
        {
            string result = string.Empty;
            LogicBase response = new LogicBase();
            string sqlVertify = string.Format("SELECT * FROM VALIDATION WHERE PHONEID='{0}'", phoneID);
            DataTable table = util.ExecuteDataTable(sqlVertify, null);
            List<ValidationSet> _table = EntityReader.GetEntities<ValidationSet>(table);
            if ( _table.Count == 0 )
            {
                response.ErrorType = (int)Logic.ErrorType.ValidateCodeError;
                response.ErrorID = (int)Logic.ErrorID.ValidateCodeFailure;
                response.ReturnMessage = "No Phone No Way";
                result = SerializationHelper<LogicBase>.Serialize(response);
                return result;
            }
            else if (_table[0].ValidationCode != validationCode.ToLower())
            {
                response.ErrorType = (int)Logic.ErrorType.ValidateCodeError;
                response.ErrorID = (int)Logic.ErrorID.ValidateCodeFailure;
                response.ReturnMessage = "验证码错误";
                result = SerializationHelper<LogicBase>.Serialize(response);
                return result;
            }

            string identfySql = string.Format("SELECT * FROM USER WHERE EMAIL='{0}'", email);
            int userCounts = EntityReader.GetEntities<User>(util.ExecuteDataTable(identfySql, null)).Count;
            if (userCounts > 0)
            {
                response.ErrorType = (int)Logic.ErrorType.UserExists;
                response.ErrorID = (int)Logic.ErrorID.UserExists;
                response.ReturnMessage = "邮箱地址已注册";
                result = SerializationHelper<LogicBase>.Serialize(response);
                return result;
            }

            string time = string.Format("{0}/{1}/{2} {3}:{4}:{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            string sqlInsert = string.Format("INSERT INTO USER VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", email, userName, pwd, phone, phoneID, time);
            try
            {
                util.ExecuteNonQuery(sqlInsert, null);
            }
            catch (Exception e)
            {
                LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < RegisterUser()>, MSG : {0} ", e.Message));
            }

            response.ErrorType = (int)Logic.ErrorType.NoException;
            response.ErrorID = (int)Logic.ErrorID.NoError;
            response.ReturnMessage = "success";
            result = SerializationHelper<LogicBase>.Serialize(response);
            return result;
        }
        #endregion

        #region [WebMethod] GetValidationCode GetNextValidationCode
        /// <summary>
        /// 获得验证码
        /// </summary>
        [WebMethod]
        public string GetValidationCode(string phoneID, string password)
        {
            string result = string.Empty;
            string ValidationCode = string.Empty;

            ValidationCodeResult ValidationResult = new ValidationCodeResult();
            string ExValida = Configuration.ExValida;
            if (!KBMd5.passWordCheck(ExValida + phoneID + ExValida, password))
            {
                ValidationResult.ErrorID = (int)ErrorID.ValidateCodeFailure;
                ValidationResult.ErrorType = (int)ErrorType.ValidateCodeError;
                ValidationResult.ReturnMessage = "请不要搞破坏好不!!!";
                ValidationResult.Image = string.Empty;
                result = SerializationHelper<ValidationCodeResult>.Serialize(ValidationResult);
                return result;
            }

            DrawValidationCode image = new DrawValidationCode();
            byte[] byteArray = new byte[image.Width * image.Height];
            Stream stream = new MemoryStream(byteArray);
            image.CreateImage(stream);
            ValidationCode = image.ValidationCode.ToLower();
            {
                string time = string.Format("{0}/{1}/{2} {3}:{4}:{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                string sql = string.Format("SELECT * FROM VALIDATION WHERE PHONEID='{0}' ORDER BY REGTIME DESC", phoneID);
                DataTable table = util.ExecuteDataTable(sql, null);
                List<ValidationSet> _table = EntityReader.GetEntities<ValidationSet>(table);
                if (_table.Count >= 1)
                {
                    string sqlDelete = string.Format("DELETE FROM VALIDATION WHERE PHONEID='{0}'", phoneID);
                    try
                    {
                        util.ExecuteNonQuery(sqlDelete, null);
                    }
                    catch (Exception e)
                    {
                        LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < GetValidationCode()>, MSG : {0} ", e.Message));
                    }

                    string sqlInsert = string.Format("INSERT INTO VALIDATION VALUES('{0}','{1}','{2}')", phoneID, ValidationCode, time);
                    try
                    {
                        util.ExecuteNonQuery(sqlInsert, null);
                    }
                    catch (Exception e)
                    {
                        LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < GetValidationCode()>, MSG : {0} ", e.Message));
                    }
                }
                else
                {
                    string sqlInsert = string.Format("INSERT INTO VALIDATION VALUES('{0}','{1}','{2}')", phoneID, ValidationCode, time);
                    try
                    {
                        util.ExecuteNonQuery(sqlInsert, null);
                    }
                    catch (Exception e)
                    {
                        LogManager.WriteLog(LogManager.LogFile.Trace, string.Format("Exception : < GetValidationCode()>, MSG : {0} ", e.Message));
                    }
                }
                
            }
            ValidationResult.ErrorID = (int)ErrorID.NoError;
            ValidationResult.ErrorType = (int)ErrorType.NoException;
            ValidationResult.ReturnMessage = "success";
            ValidationResult.Image = Convert.ToBase64String(byteArray);
            result = SerializationHelper<ValidationCodeResult>.Serialize(ValidationResult);
            return result;
        }

        /// <summary>
        /// 获得下一个验证码
        /// </summary>
        [WebMethod]
        public string GetNextValidationCode(string phoneID, string password)
        {
            return GetValidationCode(phoneID, password);
        }
        #endregion
      
        #region [WebMethod] GetTodayFluctuateRate
        /// <summary>
        /// 计算股票当天涨跌幅
        /// </summary>
        [WebMethod]
        public string GetTodayFluctuateRate(string email, string passWord)
        {
            string result = string.Empty;
            StockItemResult stockItemR = new StockItemResult();
            if (!VerifyUser(email, passWord))
            {
                stockItemR.ErrorID = (int)ErrorID.ValidateUserFailure;
                stockItemR.ErrorType = (int)ErrorType.UserNameOrPWDError;
                stockItemR.ReturnMessage = "User OR Password Error";
                stockItemR.StockItems = null;
                result = SerializationHelper<StockItemResult>.Serialize(stockItemR);
                return result;
            }

            string sql = string.Format("SELECT * FROM STOCKITEM_DAILYFLUCTUATERATE");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> _table = EntityReader.GetEntities<StockItem>(table);
            List<StockItem> _tableEx = new List<StockItem>();
            foreach (StockItem item in _table)
            {
                StockItem tmp = item;
                string dt = item.StockDate;
                DateTime dateEx = Convert.ToDateTime(dt);
                string date = string.Format("{0}/{1}/{2}", dateEx.Year, dateEx.Month, dateEx.Day);
                tmp.StockDate = date;
                _tableEx.Add(tmp);
            }
            stockItemR.StockItems = _tableEx;
            stockItemR.ErrorID = (int)ErrorID.NoError;
            stockItemR.ErrorType = (int)ErrorType.NoException;
            stockItemR.ReturnMessage = "success";
            result = SerializationHelper<StockItemResult>.Serialize(stockItemR);
            return result;
        }
        #endregion

        #region [WebMethod] GetTodayChangeRate
        /// <summary>
        /// 计算股票当天换手率
        /// </summary>
        [WebMethod]
        public string GetTodayChangeRate(string email, string passWord)
        {
            string result = string.Empty;
            StockItemResult stockItemR = new StockItemResult();
            if (!VerifyUser(email, passWord))
            {
                stockItemR.ErrorID = (int)ErrorID.ValidateUserFailure;
                stockItemR.ErrorType = (int)ErrorType.UserNameOrPWDError;
                stockItemR.ReturnMessage = "User OR Password Error";
                stockItemR.StockItems = null;
                result = SerializationHelper<StockItemResult>.Serialize(stockItemR);
                return result;
            }
            
            string sql = string.Format("SELECT * FROM STOCKITEM_DAILYCHANGERATE");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<StockItem> _table = EntityReader.GetEntities<StockItem>(table);
            List<StockItem> _tableEx = new List<StockItem>();
            foreach (StockItem item in _table)
            {
                StockItem tmp = item;
                string dt = item.StockDate;
                DateTime dateEx = Convert.ToDateTime(dt);
                string date = string.Format("{0}/{1}/{2}", dateEx.Year, dateEx.Month, dateEx.Day);
                tmp.StockDate = date;
                _tableEx.Add(tmp);
            }
            stockItemR.StockItems = _tableEx;
            stockItemR.ErrorID = (int)ErrorID.NoError;
            stockItemR.ErrorType = (int)ErrorType.NoException;
            stockItemR.ReturnMessage = "success";
            result = SerializationHelper<StockItemResult>.Serialize(stockItemR);
            return result;
        }
        #endregion

        #region [WebMethod] GetRecentDaysChangeRate
        /// <summary>
        /// 获取股票**个交易日换手率 
        /// </summary>
        [WebMethod]
        public string GetRecentDaysChangeRate(string email, string passWord, int days)
        {
            string result = string.Empty;
            bool IsChangerateMain = true;
            Stockitem_Changerate_FluctuaterateResult ItemResult = new Stockitem_Changerate_FluctuaterateResult();
            if (!VerifyUser(email, passWord))
            {
                ItemResult.ErrorID = (int)ErrorID.ValidateUserFailure;
                ItemResult.ErrorType = (int)ErrorType.UserNameOrPWDError;
                ItemResult.ReturnMessage = "User OR Password Error";
                ItemResult.Stockitem_Changerate_Fluctuaterates = null;
                result = SerializationHelper<Stockitem_Changerate_FluctuaterateResult>.Serialize(ItemResult);
                return result;
            }
            string sql = string.Format("SELECT * FROM STOCKITEM_CHANGERATE_FLUCTUATERATE WHERE CHANGERATEMAIN={0} AND TRADEDAYS={1} ORDER BY CHANGERATE DESC", IsChangerateMain, days);

            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stockitem_Changerate_Fluctuaterate> _table = EntityReader.GetEntities<Stockitem_Changerate_Fluctuaterate>(table);
            ItemResult.Stockitem_Changerate_Fluctuaterates = _table;
            ItemResult.ErrorID = (int)ErrorID.NoError;
            ItemResult.ErrorType = (int)ErrorType.NoException;
            ItemResult.ReturnMessage = "success";
            result = SerializationHelper<Stockitem_Changerate_FluctuaterateResult>.Serialize(ItemResult);
            return result;
        }
        #endregion

        #region [WebMethod] GetRecentDaysFluctuateRate
        /// <summary>
        /// 获取股票**个交易日涨跌幅 
        /// </summary>
        [WebMethod]
        public string GetRecentDaysFluctuateRate(string email, string passWord, int days)
        {
            string result = string.Empty;
            bool IsChangerateMain = false;
            Stockitem_Changerate_FluctuaterateResult ItemResult = new Stockitem_Changerate_FluctuaterateResult();
            if (!VerifyUser(email, passWord))
            {
                ItemResult.ErrorID = (int)ErrorID.ValidateUserFailure;
                ItemResult.ErrorType = (int)ErrorType.UserNameOrPWDError;
                ItemResult.ReturnMessage = "User OR Password Error";
                ItemResult.Stockitem_Changerate_Fluctuaterates = null;
                result = SerializationHelper<Stockitem_Changerate_FluctuaterateResult>.Serialize(ItemResult);
                return result;
            }
            string sql = string.Format("SELECT * FROM STOCKITEM_CHANGERATE_FLUCTUATERATE WHERE CHANGERATEMAIN={0} AND TRADEDAYS={1} ORDER BY FLUCTUATERATE DESC", IsChangerateMain, days);

            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stockitem_Changerate_Fluctuaterate> _table = EntityReader.GetEntities<Stockitem_Changerate_Fluctuaterate>(table);
            ItemResult.Stockitem_Changerate_Fluctuaterates = _table;
            ItemResult.ErrorID = (int)ErrorID.NoError;
            ItemResult.ErrorType = (int)ErrorType.NoException;
            ItemResult.ReturnMessage = "success";
            result = SerializationHelper<Stockitem_Changerate_FluctuaterateResult>.Serialize(ItemResult);
            return result;
        }
        #endregion

        private bool VerifyUser(string email, string passWord)
        {
            bool vertified = false;
            string sql = string.Format("SELECT * from USER where EMAIL='{0}' and PASSWORD='{1}'", email, passWord);
            DataTable table = util.ExecuteDataTable(sql, null);
            List<User> _table = EntityReader.GetEntities<User>(table);
            if (_table.Count == 1)
            {
                vertified = true;
            }
            return vertified;
        }



        private void InitConnection()
        {

        }

        #region [WebMethod] TestService
        /// <summary>
        /// 获取股票**个交易日涨跌幅 
        /// </summary>
        [WebMethod]
        public string TestService(string Name)
        {
            return Name + "visit success";
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataDispacher.Logic;

namespace DataDispacher
{
    public class LogicBase
    {
        #region All the Properties
        private int m_ErrorType;

        public int ErrorType
        {
            get
            {
                return m_ErrorType; 
            }
            set
            {
                m_ErrorType = value;
            }
        }

        private int m_ErrorID;

        public int ErrorID
        {
            get
            {
                return m_ErrorID;
            }
            set
            {
                m_ErrorID = value;
            }
        }

        private string m_ReturnMessage;

        public string ReturnMessage
        {
            get
            {
                return m_ReturnMessage;
            }
            set
            {
                m_ReturnMessage = value;
            }
        }
        #endregion
        
        public LogicBase()
        {
            m_ErrorType = (int)Logic.ErrorType.NoException;
            m_ErrorID = (int)Logic.ErrorID.NoError;
            m_ReturnMessage = string.Empty;
        }
    }
}
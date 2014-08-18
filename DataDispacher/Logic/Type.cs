using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDispacher.Logic
{
    public class Type
    {
        public static readonly DateTime DT_COM_MIN = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
    }

    public enum ErrorType : int
    {
        NoException = 0,
        DatabaseException = 1,
        UserExists = 2,
        OtherThanDatabaseException = 3,
        UserNameOrPWDError = 4,
    }

    public enum ErrorID : int
    {
        NoError = 0,
        DatabaseException = 1,
        UserExists = 2,
        ValidateSerializeFailure = 3,
        ValidateUserFailure = 4,
    }

    public enum Test : int
    {
        TestCode = 0,
    }
}
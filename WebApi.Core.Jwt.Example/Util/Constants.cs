using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core.Jwt.Example.Util
{
    public static class Constants
    {
        public const string CORS = "AllowAllHeaders";
        public const string _JWT_KEY = "AbCd101alkdjfalsjdfçlasdfçlkasd0";
        public const string _JWT_ISSUER = "https://localhost:80/";
        public const string _JWT_AUD = "Chrome";
        public const int _JWT_EXP_HOUR = 1;
    }
}

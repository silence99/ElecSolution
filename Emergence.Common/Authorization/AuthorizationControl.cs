using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Common;
using Emergence.Common.Model;
using System.Security.Cryptography;
using System.IO;

namespace Emergence.Common.Authorization
{
    public static class AuthorizationControl
    {
        private static LoginInModel LoginInfo;

        public static string GetAuthorization(string signString)
        {
            if(LoginInfo == null)
            {
                return string.Empty;
            }
            else
            {
                string text = signString; //"token=" + LoginInfo.Token + "&signature=" + LoginInfo.Sign;
                string key = LoginInfo.TokenSecret;
                string sign = HmacSha1Sign(text, key);
                string result = Convert.ToBase64String(Encoding.UTF8.GetBytes("token=" + LoginInfo.Token + "&signature=" + sign));
                return result;
            }
        }

        public static void SetLogin(LoginInModel lm)
        {
            if (LoginInfo == null)
            {
                LoginInfo = lm;
            }
        }

        private static string HmacSha1Sign(string text, string key)
        {
            Encoding encode = Encoding.UTF8;
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            return Convert.ToBase64String(hmac.Hash);
        }

    }
}

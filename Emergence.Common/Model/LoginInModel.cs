using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class LoginInModel
    {
        private int _code;
        private string _message;
        private string _nick;
        private string _region;
        private string _token;
        private string _tokenSecret;
        private string _userId;
        private string _sign;
        private string _Authorization;

        public string Nick { get => _nick; set => _nick = value; }
        public string Region { get => _region; set => _region = value; }
        public string Token { get => _token; set => _token = value; }
        public string TokenSecret { get => _tokenSecret; set => _tokenSecret = value; }
        public string UserId { get => _userId; set => _userId = value; }
        public string Authorization { get => _Authorization; set => _Authorization = value; }
        public string Sign { get => _sign; set => _sign = value; }
        public int Code { get => _code; set => _code = value; }
        public string Message { get => _message; set => _message = value; }
    }
}

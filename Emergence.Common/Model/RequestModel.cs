using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class RequestModel
    {
        private string _XProjectDate;
        private string _token;
        private string _tokenSecret;
        private string _sign;
        private string _authorization;
        private string _url;

        public string XProjectDate { get => _XProjectDate; set => _XProjectDate = value; }
        public string Token { get => _token; set => _token = value; }
        public string Sign { get => _sign; set => _sign = value; }
        public string TokenSecret { get => _tokenSecret; set => _tokenSecret = value; }
        public string Authorization { get => _authorization; set => _authorization = value; }
        public string Url { get => _url; set => _url = value; }
    }
}

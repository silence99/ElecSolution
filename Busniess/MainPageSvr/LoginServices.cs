using Emergence.Common.Model;
using Framework.Http;
using System.Collections.Generic;

namespace Busniess.MainPageSvr
{
    public class LoginServices: RemoteService<LoginInModel, Dictionary<string,string>>
    {
        protected override void InitializeHttpRequest(Dictionary<string, string> requestInfo)
        {
            // this need implement when call remote http service
        }

        protected override HttpResult HttpReqeust()
        {
            // this need implement when call remote http service
            return null;
        }

        protected override LoginInModel AnalyzeResponse(HttpResult result)
        {
            LoginInModel login = new LoginInModel();
            return login;
        }
    }
}

using Emergence.Common.Model;
using Framework.Http;
using System.Collections.Generic;

namespace Busniess.MainPageSvr
{
    public class MasterEventService : RemoteService<List<MasterEvent>, object>
    {
        protected override void InitializeHttpRequest(object request)
        {
            // this need implement when call remote http service
        }

        protected override HttpResult HttpReqeust()
        {
            // this need implement when call remote http service
            return null;
        }

        protected override List<MasterEvent> AnalyzeResponse(HttpResult result)
        {
            List<MasterEvent> events = new List<MasterEvent>()
            {
                new MasterEvent()
                {
                     ID = 0,
                     Describe = "description"
                }
            };
            return events;
        }
    }
}

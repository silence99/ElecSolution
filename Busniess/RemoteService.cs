﻿using System;
using Framework.Http;

namespace Business
{
    public abstract class RemoteService<Response, Request> : IRemoteService<Response, Request>
    {
        public HttpHelper HttpHelper { get; set; }

        public RemoteService()
        {
            HttpHelper = new HttpHelper();
        }

        protected virtual void InitializeHttpRequest(Request request) { throw new NotImplementedException(); }
        protected virtual HttpResult HttpReqeust() { throw new NotImplementedException(); }
        protected virtual Response AnalyzeResponse(HttpResult result) { throw new NotImplementedException(); }

        public Response ProcessRequest(Request request)
        {
            InitializeHttpRequest(request);
            var response = HttpReqeust();
            return AnalyzeResponse(response);
        }
    }
}

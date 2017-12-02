using Framework.Http;

namespace Business
{
    public interface IRemoteService<Response, Request>: IService<Response, Request>
    {
        HttpHelper HttpHelper { get; set; }
    }
}

using Framework.Http;

namespace Busniess
{
    public interface IRemoteService<Response, Request>: IService<Response, Request>
    {
        HttpHelper HttpHelper { get; set; }
    }
}

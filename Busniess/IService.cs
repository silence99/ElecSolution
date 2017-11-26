namespace Busniess
{
    public interface IService<Response, Request>
    {
        Response ProcessRequest(Request request);
    }
}

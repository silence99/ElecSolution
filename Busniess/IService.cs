namespace Business
{
    public interface IService<Response, Request>
    {
        Response ProcessRequest(Request request);
    }
}

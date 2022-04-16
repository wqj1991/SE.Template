namespace Hi.Http.Client;

public interface IHttpClientProxy<out TRemoteService>
{
    TRemoteService Service { get; }
}

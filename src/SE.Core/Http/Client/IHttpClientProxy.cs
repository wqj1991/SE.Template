namespace SE.Http.Client;

public interface IHttpClientProxy<out TRemoteService>
{
    TRemoteService Service { get; }
}

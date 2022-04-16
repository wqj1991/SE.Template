﻿namespace Hi.Http.Client;

public class HttpClientProxy<TRemoteService> : IHttpClientProxy<TRemoteService>
{
    public TRemoteService Service { get; }
    
    

    public HttpClientProxy(TRemoteService service)
    {

        
        Service = service;
        


    }
}

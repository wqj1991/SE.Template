using System.Reflection;
using Furion;
using Furion.DependencyInjection;
using Furion.Reflection;
using Furion.RemoteRequest;
using Hi.RemoteRequest.Proxies;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace SE.MyProjectName.Host;

public static class HttpClientSetup
{
    /// <summary>
    /// 创建代理方法
    /// </summary>
    private static readonly MethodInfo DispatchCreateMethod;

    /// <summary>
    /// 全局服务代理类型
    /// </summary>
    private static readonly Type GlobalServiceProxyType;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static HttpClientSetup()
    {
        // 获取全局代理类型
        GlobalServiceProxyType = App.EffectiveTypes
            .FirstOrDefault(u =>
                typeof(HiAspectDispatchProxy).IsAssignableFrom(u) && typeof(IGlobalDispatchProxy).IsAssignableFrom(u) &&
                u.IsClass && !u.IsInterface && !u.IsAbstract);

        DispatchCreateMethod = typeof(HiAspectDispatchProxy).GetMethod(nameof(HiAspectDispatchProxy.Create));
    }


    public static IServiceCollection AddHttpClientSetup(this IServiceCollection services)
    {
        if (App.Configuration.GetSection("RemoteServices").GetChildren().Count() == 0)
            return services;
        
        // 注册远程请求代理接口
        services.AddScopedDispatchProxyForInterface<HiHttpDispatchProxy, IHttpDispatchProxy>();

        // 注册默认请求客户端
        services.AddHttpClient();

        return services;
    }

    /// <summary>
    /// 添加接口代理
    /// </summary>
    /// <typeparam name="TDispatchProxy">代理类</typeparam>
    /// <typeparam name="TIDispatchProxy">被代理接口依赖</typeparam>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddScopedDispatchProxyForInterface<TDispatchProxy, TIDispatchProxy>(
        this IServiceCollection services)
        where TDispatchProxy : HiAspectDispatchProxy, IDispatchProxy
        where TIDispatchProxy : class
    {
        // 注册代理类
        services.AddScoped<HiAspectDispatchProxy, TDispatchProxy>();

        // 代理依赖接口类型
        var proxyType = typeof(TDispatchProxy);

        var remoteServices = App.Configuration.GetSection("RemoteServices");

        foreach (IConfigurationSection section in remoteServices.GetChildren())
        {
            var value = section.GetValue<string>("BaseUrl");
            
            // 获取所有的代理接口类型
            var dispatchProxyInterfaceTypes = App.EffectiveTypes
                .Where(e => e.IsInterface && e.FullName.Contains( section.GetValue<string>("Project")));

            // 注册代理类型
            foreach (var interfaceType in dispatchProxyInterfaceTypes)
            {
                AddDispatchProxy(services, typeof(IScoped), default, proxyType, interfaceType, false);
            }
        }



        return services;
    }

    /// <summary>
    /// 创建服务代理
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="dependencyType"></param>
    /// <param name="type">拦截的类型</param>
    /// <param name="proxyType">代理类型</param>
    /// <param name="inter">代理接口</param>
    /// <param name="hasTarget">是否有实现类</param>
    private static void AddDispatchProxy(IServiceCollection services, Type dependencyType, Type type, Type proxyType,
        Type inter, bool hasTarget = true)
    {
        proxyType ??= GlobalServiceProxyType;
        if (proxyType == null || (type != null && type.IsDefined(typeof(SuppressProxyAttribute), true))) return;

        // 注册代理类型
        services.InnerAdd(dependencyType, typeof(HiAspectDispatchProxy), proxyType);

        // 注册服务
        services.InnerAdd(dependencyType, inter, provider =>
        {
            dynamic proxy = DispatchCreateMethod.MakeGenericMethod(inter, proxyType).Invoke(null, null);
            proxy.Services = provider;
            if (hasTarget)
            {
                proxy.Target = provider.GetService(type);
            }

            return proxy;
        });
    }

    /// <summary>
    /// 注册服务（如果服务存在，覆盖注册）
    /// </summary>
    /// <param name="dependencyType"></param>
    /// <param name="collection"></param>
    /// <param name="service"></param>
    internal static IServiceCollection InnerAdd(this IServiceCollection collection, Type dependencyType, Type service)
    {
        Call(dependencyType, MethodBase.GetCurrentMethod()
            , new object[] { collection, service });

        return collection;
    }

    /// <summary>
    /// 注册服务（如果服务存在，覆盖注册）
    /// </summary>
    /// <param name="dependencyType"></param>
    /// <param name="collection"></param>
    /// <param name="service"></param>
    /// <param name="implementationType"></param>
    internal static IServiceCollection InnerAdd(this IServiceCollection collection, Type dependencyType, Type service,
        Type implementationType)
    {
        Call(dependencyType, MethodBase.GetCurrentMethod()
            , new object[] { collection, service, implementationType });

        return collection;
    }

    /// <summary>
    /// 注册服务（如果服务存在，覆盖注册）
    /// </summary>
    /// <param name="dependencyType"></param>
    /// <param name="collection"></param>
    /// <param name="service"></param>
    /// <param name="implementationFactory"></param>
    internal static IServiceCollection InnerAdd(this IServiceCollection collection, Type dependencyType, Type service,
        Func<IServiceProvider, object> implementationFactory)
    {
        Call(dependencyType, MethodBase.GetCurrentMethod()
            , new object[] { collection, service, implementationFactory });

        return collection;
    }

    /// <summary>
    /// 反射调用
    /// </summary>
    /// <param name="dependencyType">dependencyType</param>
    /// <param name="currentMethod"></param>
    /// <param name="args"></param>
    /// <param name="genericArguments"></param>
    private static void Call(Type dependencyType, MethodBase currentMethod, object[] args,
        Type[] genericArguments = default)
    {
        var tryWay = currentMethod.Name.StartsWith("InnerTry");
        var methodName = $"{currentMethod.Name[5..]}{dependencyType.Name[1..]}";

        // 获取方法签名（很笨的方式）
        var methodSignature = currentMethod.ToString()
            .Replace($"IServiceCollection Inner{(tryWay ? "Try" : string.Empty)}Add",
                $"IServiceCollection {methodName}")
            .Replace("Microsoft.Extensions.DependencyInjection.IServiceCollection, System.Type",
                "Microsoft.Extensions.DependencyInjection.IServiceCollection");

        // 调用静态方法
        Invoke(tryWay ? typeof(ServiceCollectionDescriptorExtensions) : typeof(ServiceCollectionServiceExtensions)
            , methodSignature
            , genericArguments
            , args);
    }

    /// <summary>
    /// 反射调用微软内部注册服务方法
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodSignature"></param>
    /// <param name="genericParameters"></param>
    /// <param name="args"></param>
    private static void Invoke(Type type, string methodSignature, Type[] genericParameters, object[] args = null)
    {
        var isGeneric = genericParameters != null && genericParameters.Length > 0;

        // 查找符合方法签名的方法
        var method = type.GetMethods()
            .Where(m => m.ToString().Equals(methodSignature))
            .FirstOrDefault() ?? throw new InvalidOperationException($"Not found method: {methodSignature}.");

        // 处理泛型
        var realMethod = method?.IsGenericMethod == true ? method.MakeGenericMethod(genericParameters) : method;
        realMethod?.Invoke(null, args ?? Array.Empty<object>());
    }
}
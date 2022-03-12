using Furion;
using SqlSugar;

namespace SE.MyProjectName.Host;

public static class SqlsugarSetup
{
    public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration,
        string dbName = "Default")
    {
        var con = new ConnectionConfig
        {
            //ConnectionString = "HOST=localhost;PORT=5432;DATABASE=pop;USER ID=postgres;PASSWORD=P@ssw2rd;",
            ConnectionString = configuration.GetConnectionString(dbName),
            DbType = DbType.Sqlite,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute,
            AopEvents = new AopEvents
            {
                OnLogExecuting = (sql, pars) =>
                {
                    var sqlResult = SqlProfiler.ParameterFormat(sql, pars);
                    App.PrintToMiniProfiler("SqlSugar", "Info", sqlResult);
                }
            }
        };
        
        services.AddScoped<ISqlSugarRepository, SqlSugarRepository>();
        services.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
        
        // // 注册非泛型仓储
        // services.AddScoped<IPgSqlSugarRepository, PgSqlSugarRepository>();
        //
        // // 注册 SqlSugar 仓储
        // services.AddScoped(typeof(IPgSqlSugarRepository<>), typeof(PgSqlSugarRepository<>));

        services.AddSqlSugar(con); //这边是SqlSugarScope用AddSingleton
    }
}
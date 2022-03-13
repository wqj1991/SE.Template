using Furion;
using SE.Http.Client;
using SE.MyProjectName.Host;

var builder = WebApplication.CreateBuilder(args).Inject();

var services = builder.Services;

services.AddScoped(typeof(IHttpClientProxy<>), typeof(HttpClientProxy<>));

services.AddControllers().AddInject();

services.AddSqlsugarSetup(App.Configuration);

services.AddHttpClientSetup();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseInject("swagger");

app.MapControllers();

app.Run();
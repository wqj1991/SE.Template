using Furion;
using SE.MyProjectName.Host;

var builder = WebApplication.CreateBuilder(args).Inject();

var services = builder.Services;

services.AddControllers().AddInject();

services.AddSqlsugarSetup(App.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseInject();

app.MapControllers();

app.Run();
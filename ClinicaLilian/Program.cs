using ClinicaLilian.Configuration;
using Data.Context;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Conexão com o banco de dados (Sql Server)
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(connection));

var apiSettingsConfig = builder.Configuration.GetSection("ConnectionStrings").Get<DatabaseSettings>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSingleton(apiSettingsConfig);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDependencyInjectionConfiguration();

//builder.Services.AddControllers(options =>
//{
//    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
//});

var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logg");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.File(
            path: Path.Combine(logDirectory, "information.log"),
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: null,
            shared: true)
    )
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(
            path: Path.Combine(logDirectory, "error.log"),
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: null,
            shared: true)
    )
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Newtonsoft.Json.Formatting.Indented,
    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
};

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

global using MyAspDotNet6App.Common;
global using static MyAspDotNet6App.Common.Constants;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.Logging;
using MyAspDotNet6App.SqlDataAccess;
using MyAspDotNet6App.Utilities;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
       .AddNegotiate();

    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = options.DefaultPolicy;
    });
    builder.Services.AddRazorPages();

    builder.Services.AddTransient<UserNameEnricher>();
    builder.Services.AddHttpContextAccessor();
    builder.Host
        .UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) =>
            loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.With(serviceProvider.GetService<UserNameEnricher>())
                .ReadFrom.Configuration(builder.Configuration));

    var context = new MyAppContext(builder.Configuration.GetConnectionString("MyDatabaseConnectionString"));
    builder.Services.AddSingleton(context);
    builder.Services.AddSingleton<IExcelCreator, ExcelCreator>();
    builder.Services.AddSingleton<IDepartmentRepository, SqlDepartmentRepository>();
    builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
    builder.Services.AddSingleton<IMemberRepository, SqlMemberRepository>();
    builder.Services.AddSingleton<IMemberService, MemberService>();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "起動時に予期しないエラーが発生しました");
}
finally
{
    Log.CloseAndFlush();
}

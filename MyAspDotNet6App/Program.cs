﻿global using MyAspDotNet6App.Common;
global using static MyAspDotNet6App.Common.Constants;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.SqlDataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();

var context = new MyAppContext(builder.Configuration.GetConnectionString("MyDatabaseConnectionString"));
builder.Services.AddSingleton(context);
builder.Services.AddSingleton<IMemberRepository, SqlMemberRepository>();
builder.Services.AddSingleton<IMemberService, MemberService>();
builder.Services.AddSingleton<IDepartmentRepository, SqlDepartmentRepository>();
builder.Services.AddSingleton<IDepartmentService, DepartmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

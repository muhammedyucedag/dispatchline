using DispatchLine.Application;
using DispatchLine.Domain.Entities.Identity;
using DispatchLine.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// app.Use(async (context, next) =>
// {
//     if (context.Request.Path == "/" || context.Request.Path == "/index.html")
//     {
//         context.Response.Redirect("/scalar/v1");
//         return;
//     }
//
//     await next();
// });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("DispatchLine API")
            .WithDownloadButton(true)
            .WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseCors("DefaultPolicy");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapWhen(context => context.Request.Path == "/" || context.Request.Path == "/index.html",
    appBuilder =>
    {
        appBuilder.Run(context =>
        {
            context.Response.Redirect("/scalar/v1");
            return Task.CompletedTask;
        });
    });

app.MapControllers();

app.Run();
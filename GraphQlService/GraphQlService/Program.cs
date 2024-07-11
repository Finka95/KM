using GraphQlService.BLL.DI;
using GraphQlService.Queries;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GraphQlService;
using GraphQlService.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => 
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["app.at"];
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters.ValidateAudience = false;
    });

builder.Services.AddHttpResponseFormatter<CustomHttpResponseFormatter>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddErrorFilter<ErrorFilter>()
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddHttpClient();

builder.Services.RegisterBusinessLogicDependencies(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGraphQL();

app.Run();

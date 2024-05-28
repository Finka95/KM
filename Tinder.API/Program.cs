using System.Text.Json.Serialization;
using Tinder.API.Extension;
using Tinder.API.Hubs;
using Tinder.API.Mapper;
using Tinder.API.Middleware;
using Tinder.BLL.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    options.Configuration = connection;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Host.AddSerilogConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen();
builder.Services.RegisterBusinessLogicDependencies(builder.Configuration);

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Events = new()
        {
            OnMessageReceived = context =>
            {
                // Extract the token from a cookie if available.
                context.Token = context.Request.Cookies["app.at"];
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chat");
app.MapControllers();
app.UseCors();
app.Run();

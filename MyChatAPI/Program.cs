
using GroupChatApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyChatAPI.Context;
using MyChatAPI.Services;
using MyChatAPI.SingalR;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddDbContext<ClientDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyChatDb"));
});
builder.Services.AddSignalR();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/hubs"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors(x => 
x.AllowAnyHeader()
    //.AllowCredentials()o
    .AllowAnyMethod()
    .AllowAnyOrigin()
    //.WithOrigins("http://localhost:4200")
    );

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<PresenceHub>("hubs/presence");
});
app.MapControllers();

app.Run();

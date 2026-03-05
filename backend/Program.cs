using backend.DBContext;
using backend.Services.Access;
using backend.Services.Cast;
using backend.Services.Movie;
using backend.Services.Rol;
using backend.Services.UserApp;
using backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS
string corsOrigin = builder.Configuration["CorsOrigin:Url"]!;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5173", "https://localhost:7221")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod());
});
#endregion

#region Json web token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["AT"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSingleton<TokenManager>();
#endregion

#region Logging
Log.Logger =
    new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddSingleton<LogManager>();
#endregion

#region Database
var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection");
builder.Services.AddDbContext<MovieCatalogContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region Utils
builder.Services.AddTransient<HttpContextUtils>();
builder.Services.AddTransient<JsonManager>();
builder.Services.AddTransient<Security>();
#endregion

#region DI User
builder.Services.AddTransient<GetAllUsers>();
builder.Services.AddTransient<GetUserById>();
builder.Services.AddTransient<PostNewUser>();
#endregion

#region DI Access
builder.Services.AddTransient<Login>();
builder.Services.AddTransient<Signup>();
#endregion

#region DI Movie
builder.Services.AddTransient<GetAllMovies>();
builder.Services.AddTransient<GetMovieById>();
builder.Services.AddTransient<PostNewMovie>();
builder.Services.AddTransient<PutMovie>();
#endregion

# region DI Rol
builder.Services.AddTransient<GetAllRoles>();
builder.Services.AddTransient<GetRolById>();
builder.Services.AddTransient<PostNewRol>();
builder.Services.AddTransient<PutRol>();
builder.Services.AddTransient<DeleteRol>();
#endregion

#region DI Cast
builder.Services.AddTransient<GetAllCast>();
builder.Services.AddTransient<GetCastById>();
builder.Services.AddTransient<PostNewCast>();
builder.Services.AddTransient<DeleteCast>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.Equals(404) && !System.IO.Path.HasExtension(context.Request.Path.Value))
    {
        context.Request.Path = "/index.html";
        await next();
    }
});
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

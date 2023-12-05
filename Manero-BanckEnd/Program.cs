using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Manero_BanckEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<TokenGenerator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };
    })
    .AddOAuth("Google", options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
        options.CallbackPath = new PathString("/signin-google");

        options.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
        options.TokenEndpoint = "https://accounts.google.com/o/oauth2/token";

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");

        options.SaveTokens = true;
        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

        options.Events = new OAuthEvents();
    })
    .AddOAuth("Facebook", options =>
    {
        options.ClientId = builder.Configuration["Authentication:Facebook:AppId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!;
        options.CallbackPath = new PathString("/signin-facebook");

        options.AuthorizationEndpoint = "https://www.facebook.com/v12.0/dialog/oauth";
        options.TokenEndpoint = "https://graph.facebook.com/v12.0/oauth/access_token";
        options.UserInformationEndpoint = "https://graph.facebook.com/v12.0/me";

        options.Scope.Add("email");
        options.Scope.Add("public_profile");

        options.SaveTokens = true;
        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

        options.Events = new OAuthEvents();
    })
    .AddOAuth("Twitter", options =>
    {
        options.ClientId = builder.Configuration["Authentication:Twitter:ConsumerKey"]!;
        options.ClientSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"]!;
        options.CallbackPath = new PathString("/signin-twitter");

        options.AuthorizationEndpoint = "https://api.twitter.com/oauth/authenticate";
        options.TokenEndpoint = "https://api.twitter.com/oauth/access_token";
        options.UserInformationEndpoint = "https://api.twitter.com/1.1/account/verify_credentials.json";

        options.SaveTokens = true;

        options.Events = new OAuthEvents();
    });

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "Manero API",
        Description = "API for Manero",
        Contact = new OpenApiContact
        {
            Name = "Group 3",
        },
    });

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();


builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<ProfileRepo>();
builder.Services.AddScoped<ApiKeyRepo>();
builder.Services.AddScoped<UseApiKeyAttribute>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<CardRepo>();
builder.Services.AddScoped<PromoCodeRepo>();
builder.Services.AddScoped<PromoCodeService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<TokenRepo>();
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddScoped<DataInitializer>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<AddressRepo>();
builder.Services.AddScoped<AddressTypeRepo>();
builder.Services.Configure<PhoneVerification>(builder.Configuration.GetSection("Twilio"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().MigrateData();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
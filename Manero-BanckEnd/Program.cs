using Manero_BanckEnd;
using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
   
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x=>
    {
        x.TokenValidationParameters = new TokenValidationParameters
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
    });
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "Manero API",
        Description = @"API for Manero",
        Contact = new OpenApiContact
        {
            Name = "Group 3",

        },
    });

});
builder.Services.AddAuthorization();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<UserRepo>(); 
builder.Services.AddScoped<ApiKeyRepo>();
builder.Services.AddScoped<UseApiKeyAttribute>();


var app = builder.Build();

app.UseCors(x=> x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

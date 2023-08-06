using JWTAuthentication.NET6._0.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using API.Utility;
using BLL.DTOS.Search;
using DAL.Interfaces;
using DAL.Services;
using BLL.DTOS.Addresses;
using BLL.DTOS.Contacts;
using BLL.DTOS.Client;
using BLL.DTOS.Emails;
using BLL.DTOS.Order;

var builder = WebApplication.CreateBuilder(args);
    var config = builder.Configuration;
    
    var configuration = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .Build();

    var connectionString = configuration.GetConnectionString("EmployeeDbConnection");
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Client, ClientDTO>();
       // .ForAllMembers(opt => opt.ExplicitExpansion());
    cfg.CreateMap<Client, SearchResultDTO>();
    cfg.CreateMap<ClientAddresses, ClientAddressesDTO>();
    cfg.CreateMap<ClientContact, ClientContactDTO>(); 
    cfg.CreateMap<ClientEmail, ClientEmailDTO>(); 
    cfg.CreateMap<Order, OrderDTO>();
    cfg.CreateMap<OrderCreateDTO, OrderDTO>();
    cfg.CreateMap<ClientAddressCreateDTO, ClientAddresses>();
    cfg.CreateMap<ClientContactCreateDTO, ClientContact>();
    cfg.CreateMap<ClientEmailCreateDTO, ClientEmail>();
    cfg.CreateMap<Client, ClientDDTO>();
});
AppDomain.CurrentDomain.GetAssemblies();

    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
     builder.Services.AddScoped<IClientAddressService, ClientAddressService>();
    builder.Services.AddScoped<IClientContactService, ClientContactService>();
    builder.Services.AddScoped<IClientEmailService, ClientEmailService>();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new List<string>()
            }
        });
    });
    builder.Services.AddControllers();


var app = builder.Build();


if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");

            c.OAuthClientId("swagger");
            c.OAuthAppName("Swagger UI");
        }); 
    }   

    app.UseMiddleware<GlobalErrorHandlingMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

app.Run();

using AlatAuth.Business.Adapter;
using AlatAuth.Business.Interface;
using AlatAuth.Business.Service.Implementation;
using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.RepositoryPattern.Implementation;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.DataAccess;
using AlatAuth.Data.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AlatAuth.Api.ServiceInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection ServiceInject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

            // Add services to the container.

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });


            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                    .WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ICustomerRepo, CustomerReop>();
            services.AddScoped<IOtpRepo, OtpRepo>();
            services.AddScoped<ILgaRepo, LgaRepo>();
            services.AddScoped<IStateRepo, StateRepo>();
            services.AddScoped<ICustomerService, CustomerService>();


            services.AddHttpClient<GenericAdapter>();
            return services;
        }

    }
}

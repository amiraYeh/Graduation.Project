using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using GP.Focusi.Repository.Identity.Contexts;
using GP.Focusi.Services.Tokens;
using GP.Focusi.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GP.Focusi.Services;
using StackExchange.Redis;
using GP.Focusi.Repository.Data.Contexts;
using GP.Focusi.API.Mapping;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Repository.Repositories;
using GP.Focusi.Core.Entites;


namespace GP.Focusi.API.Helper
{
	public static class DepndencyInjection
	{
		public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configurations)
		{
			services.AddBiltInService();
			services.AddSwaggerService();
			services.AddDbContextService(configurations);
			services.AddUserDefinedService();
			services.AddAutoMapperService(configurations);
			services.AddIdentityService();
			services.AddAuthenticationService(configurations);
			services.AddRedisService(configurations);
			services.AddCorsService();
			return services;
		}

		private static IServiceCollection AddBiltInService(this IServiceCollection services)
		{
			services.AddControllers();
			return services;
		}
		private static IServiceCollection AddSwaggerService(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options=>
			{
				//options.SwaggerDoc(
				//	"V1",
				//	new OpenApiInfo
				//	{
				//		Title = "Focusi Api"
				//	}
				//	);
				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "Authorithation",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					Reference = new OpenApiReference
					{
						Id = "Bearer",
						Type = ReferenceType.SecurityScheme
					}
				};
				options.AddSecurityDefinition("Bearer",securityScheme);
				var securityRequirement = new OpenApiSecurityRequirement
				{
					{securityScheme,new[]{"Bearer"} }
				};
				options.AddSecurityRequirement(securityRequirement);
			});

			return services;
		}
		private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configurations)
		{
			services.AddDbContext<FocusiIdentityDbContext>(options =>
			{
				options.UseSqlServer(configurations.GetConnectionString("IdentityConection"));

			});
			services.AddDbContext<FocusiAppDbContext>(options =>
			{
				options.UseSqlServer(configurations.GetConnectionString("AppConnection"));

			});
			return services;
		}
		private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
		{
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserService, UserServise>();
			services.AddTransient<IEmailSenderService, EmailSenderService>();
			services.AddScoped<ICacheService, CacheService>();
			services.AddScoped<ITaskManagerService, TaskManagerService>();
			services.AddScoped<ITaskManagerRepository, TaskManagerRepository>();
			services.AddScoped<IFeedBackRepository, FeedBackRepository>();
			services.AddScoped<IParentTestService, ParentTestService>();
			services.AddScoped<IParentTestRepository, ParentTestRepository>();
			services.AddScoped<IStoryAndAdviceServices, StoryAndAdviceServices>();
			services.AddScoped<IStoryAndAdviceRepository<Advice>,StoryAndAdviceRepository<Advice>>();
			services.AddScoped<IStoryAndAdviceRepository<Story>, StoryAndAdviceRepository<Story>>();
			services.AddScoped<IClassServices, ClassService>();


			return services;
		}
		private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(m=>m.AddProfile(new UserChildProfile()));
			services.AddAutoMapper(m => m.AddProfile(new TaskManagerProfile()));
			return services;
		}
		private static IServiceCollection AddIdentityService(this IServiceCollection services)
		{
			services.AddIdentity<AppUserChild, IdentityRole>()
					.AddEntityFrameworkStores<FocusiIdentityDbContext>()
					.AddDefaultTokenProviders();
					//.AddGoogle();
			return services;
		}
		private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidateAudience = true,
					ValidAudience = configuration["Jwt:Audience"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			});
			return services;
		}

		private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddSingleton<IConnectionMultiplexer>(options =>
			{
				var connection = configuration.GetConnectionString("Redis");

				return ConnectionMultiplexer.Connect(connection);
			});
			return services;
		}
		private static IServiceCollection AddCorsService(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
				});
			});
			return services;
		}
		//private static IServiceCollection AddScalarService(this IServiceCollection services)
		//{


		//}
	}
}

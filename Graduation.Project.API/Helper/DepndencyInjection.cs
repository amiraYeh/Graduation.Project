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
using System.Net.Mail;
using System.Net;
using GP.Focusi.Services;


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
			services.AddIdentityService();
			services.AddAuthenticationService(configurations);

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
			return services;
		}
		private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
		{
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserService, UserServise>();
			services.AddTransient<IEmailSenderService, EmailSenderService>();
			return services;
		}

		private static IServiceCollection AddIdentityService(this IServiceCollection services)
		{
			services.AddIdentity<AppUserChild,IdentityRole>()
					.AddEntityFrameworkStores<FocusiIdentityDbContext>()
					.AddDefaultTokenProviders();
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
				//options.To
			});
			//services.AddAuthentication(MailKitAuthenticationOptions.);
			return services;
		}
		


	}
}

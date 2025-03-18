
using GP.Focusi.API.Helper;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Client;

namespace Graduation.Project.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			Configuration.Default.ApiKey.Add("api-key", builder.Configuration["BrevoEmailsApi:ApiKey"]);

			// Add services to the container.

			builder.Services.AddDependency(builder.Configuration);

			//builder.Services.AddControllers();
			//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			//builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			await app.configureMiddleWares();


			app.Run();
		}
	}
}

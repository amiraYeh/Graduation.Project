
using GP.Focusi.API.Helper;
using GP.Focusi.Repository.Repositories;
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
			//TaskManagerRepository x = new TaskManagerRepository();
			//await x.GetByEmailTaskManagerAsync("amirayehsh@gmail.com");
			// Configure the HTTP request pipeline.
			await app.configureMiddleWares();


			app.Run();
		}
	}
}

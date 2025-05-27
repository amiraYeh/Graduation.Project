using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Repository.Identity.Contexts;
using Graduation.Project.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GP.Focusi.Repository.Identity;
using GP.Focusi.Repository.Data.Contexts;
using GP.Focusi.Repository.Data;

namespace GP.Focusi.API.Helper
{
	public static class ConfigureMiddleWares
	{
		public static async Task<WebApplication> configureMiddleWares(this WebApplication app)
		{

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;

			var context = services.GetRequiredService<FocusiAppDbContext>();

			var identityContext = services.GetRequiredService<FocusiIdentityDbContext>();
			var userManager = services.GetRequiredService<UserManager<AppUserChild>>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				await context.Database.MigrateAsync();// should Add migration first
				 await FocusiDbContextSeed.seedAsync(context);

			    await identityContext.Database.MigrateAsync();
				//await FocusiIdentityDbContextSeed.SeedUserAsync(userManager);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "Ther is a problem during apply Migrations");
			}
			
			if (app.Environment.IsDevelopment())
			{
			
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			if (app.Environment.IsProduction())
			{

				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Focusi API V1");
					c.RoutePrefix = "api-docs"; // This makes docs available at /api-docs
				});
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseCors("CorsPolicy");
			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			return app;
		}
	}
}

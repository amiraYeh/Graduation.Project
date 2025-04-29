using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Repository.Identity.Contexts;
using Graduation.Project.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GP.Focusi.Repository.Identity;
using GP.Focusi.Repository.Data.Contexts;

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

			    await identityContext.Database.MigrateAsync();
				await FocusiIdentityDbContextSeed.SeedUserAsync(userManager);
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

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			return app;
		}
	}
}

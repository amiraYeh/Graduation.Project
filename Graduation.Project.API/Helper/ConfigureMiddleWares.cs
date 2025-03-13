namespace GP.Focusi.API.Helper
{
	public static class ConfigureMiddleWares
	{
		public static async Task<WebApplication> configureMiddleWares(this WebApplication app)
		{


			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			return app;
		}
	}
}

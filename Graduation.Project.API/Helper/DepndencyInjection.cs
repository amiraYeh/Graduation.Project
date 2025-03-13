namespace GP.Focusi.API.Helper
{
	public static class DepndencyInjection
	{
		public static IServiceCollection AddDependency(this IServiceCollection services)
		{
			services.AddBiltInService();
			services.AddSwaggerService();
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
			services.AddSwaggerGen();
			return services;
		}
	}
}

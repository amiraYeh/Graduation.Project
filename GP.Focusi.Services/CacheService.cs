using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GP.Focusi.Services
{
	public class CacheService : ICacheService
	{
		private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task SetChachedResponseAsync(string key, object response, TimeSpan expireTime)
		{
			if (response is null) return;

			var jsonOptions = new JsonSerializerOptions(){ PropertyNamingPolicy =JsonNamingPolicy.CamelCase };

			await _database.StringSetAsync(key, JsonSerializer.Serialize(response, jsonOptions), expireTime);
		}

		public async Task<string> GetCachedResponseAsync(string key)
		{
			
			var cachedResponse = await _database.StringGetAsync(key);

			if (cachedResponse.IsNullOrEmpty) return null;

			return cachedResponse.ToString();
		}
	}
}

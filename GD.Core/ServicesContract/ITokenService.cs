using GP.Focusi.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface ITokenService
	{
		Task<string> CreateTokenAsync(AppUserChild user,UserManager<AppUserChild> userManager);
	}
}

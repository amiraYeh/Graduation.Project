using AutoMapper;
using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites.Identity;

namespace GP.Focusi.API.Mapping
{
	public class UserChildProfile:Profile
	{
        public UserChildProfile()
        {
            CreateMap<CurrentUserDto,AppUserChild>().ReverseMap();
        }
    }
}

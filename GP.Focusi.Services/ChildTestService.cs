using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services
{
    public class ChildTestService : IChildTestService
    {
        private readonly UserManager<AppUserChild> _userManager;
        private readonly IChildTestRepository _childTestRepository;

        public ChildTestService(UserManager<AppUserChild> userManager, IChildTestRepository childTestRepository) 
        {
            _userManager = userManager;
            _childTestRepository = childTestRepository;
        }
        public async Task<int?> GameTest(string email, int gameFocusRatio)
        {
            if (email is null)
                return null;

            var chClass = await getChildClass(email);

            if (chClass is null)
                return null;

            var res = await _childTestRepository.GameChildTestAsync(email, gameFocusRatio);

            if(res < 1)
                return null;

            return res;
          
        }

        public async Task<int?> VideoTest(string email, int videoFocusRatio)
        {
            if (email is null)
                return null;
            var chClass = await getChildClass(email);

            if (chClass is not null) // user done this test before
                return null;

            var res = await _childTestRepository.VideoChildTestAsync(email, videoFocusRatio);

            if (res < 1)
                return null;

            return res;
        }
        private async Task<string> getChildClass(string email)
        {
            if (email is null)
                return null;

            var child = await _userManager.FindByEmailAsync(email);

            if (child is null)
                return null;

            string childClass = child.ChildClass;
            return childClass;
        }
    }
}

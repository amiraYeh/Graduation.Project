using GP.Focusi.Core.DTOs;
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
        private string childGameMail = "";

        public ChildTestService(UserManager<AppUserChild> userManager, IChildTestRepository childTestRepository) 
        {
            _userManager = userManager;
            _childTestRepository = childTestRepository;
        }
        public async Task<string> GameTest(string email, GameTestDto gameTestDto)
        {
            if (email is null)
                return null;

            int falsePhotos = gameTestDto.totalPhotos - gameTestDto.truePhotos;
            int gameFocusRatio = calcFocusRatio(falsePhotos, gameTestDto.truePhotos);

            var res = await _childTestRepository.GameChildTestAsync(email, gameFocusRatio);

            if(res is null)
                return null;

            return res;
          
        }

        public async Task<string> VideoTest(string email,VideoTestDto videoTestDto)
        {
            if (email is null)
                return null;
           
            int falsePhotos = videoTestDto.totalPhotos - videoTestDto.truePhotos;
            int videoFocusRatio = calcFocusRatio(falsePhotos, videoTestDto.truePhotos);

            var res = await _childTestRepository.VideoChildTestAsync(email, videoFocusRatio);

            if (res is null)
                return null;

            return res;
        }
        private int calcFocusRatio(int falsePhotos, int truePhotos)
        {
            int focusRatio = 0;

            if (truePhotos < falsePhotos)
                focusRatio = 30;
            else if (truePhotos == falsePhotos)
                focusRatio = 50;

            else if (truePhotos > falsePhotos)
                focusRatio = 60;

            return focusRatio;
        }
        }
}

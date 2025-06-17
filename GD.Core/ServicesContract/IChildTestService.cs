using GP.Focusi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
    public interface IChildTestService
    {
        public Task<string> GameTest(string email, GameTestDto gameTestDto);
        public Task<string> VideoTest(string email ,VideoTestDto videoTestDto);
    }
}

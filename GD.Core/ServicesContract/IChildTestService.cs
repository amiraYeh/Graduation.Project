using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
    public interface IChildTestService
    {
        public Task<int?> GameTest(string email, int gameFocusRatio);
        public Task<int?> VideoTest(string email ,int videoFocusRatio);
    }
}

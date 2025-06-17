using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
    public interface IChildTestRepository
    {
        public Task<string> GameChildTestAsync(string email, int gameScore);
        public Task<string> VideoChildTestAsync(string email, int videoScore);
    }
}

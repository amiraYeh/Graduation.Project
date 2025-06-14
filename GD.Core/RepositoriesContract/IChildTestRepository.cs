using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.RepositoriesContract
{
    public interface IChildTestRepository
    {
        public Task<int> GameChildTestAsync(string email, int gameScore);
        public Task<int> VideoChildTestAsync(string email, int videoScore);
    }
}

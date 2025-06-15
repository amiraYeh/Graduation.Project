using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
    public interface IRoleServices
    {
        public Task<int> createRolesAsync(string roleName);
        public Task<int> editRolesAsync(string roleName);
        public Task<int> deleteRolesAsync(string roleName);
        public Task<List<IdentityRole>> getAllRolesAsync();
        public Task<bool>IsARole(string roleName);

    }
}

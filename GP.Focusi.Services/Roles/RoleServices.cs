using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services.Roles
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<IdentityRole>> getAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        public async Task<int> createRolesAsync(string roleName)
        {
            var role = await IsARole(roleName);
            if (role)
                return -1;
           var res = await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
           
           if (!res.Succeeded)
                return -1;

            return 1;
        }
        public Task<int> editRolesAsync(string roleName)
        {
            throw new NotImplementedException();
        }
        public Task<int> deleteRolesAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsARole(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}

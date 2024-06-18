using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Entities;

namespace Ultatel.Core.Interfaces
{
    public interface IAuthService
    {
        public Task<string> CreatTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}

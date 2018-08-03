using IdentityWithNullApplication.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace IdentityWithNullApplication.Models
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                                IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            manager.EmailService = new EmailService();
            var provider = new MachineKeyProtectionProvider();
            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("ASP.NET Identity"));
            return manager;
        }

        public override Task<IdentityResult> AddToRoleAsync(string userId, string role)
        {
            return base.AddToRoleAsync(userId, role);
        }

        public override Task<IdentityResult> RemoveFromRoleAsync(string userId, string role)
        {
            return base.RemoveFromRoleAsync(userId, role);
        }

        public class MachineKeyProtectionProvider : IDataProtectionProvider
        {
            public IDataProtector Create(params string[] purposes)
            {
                return new MachineKeyDataProtector(purposes);
            }
        }

        public class MachineKeyDataProtector : IDataProtector
        {
            private readonly string[] _purposes;

            public MachineKeyDataProtector(string[] purposes)
            {
                _purposes = purposes;
            }

            public byte[] Protect(byte[] userData)
            {
                return MachineKey.Protect(userData, _purposes);
            }

            public byte[] Unprotect(byte[] protectedData)
            {
                return MachineKey.Unprotect(protectedData, _purposes);
            }
        }
    }
}
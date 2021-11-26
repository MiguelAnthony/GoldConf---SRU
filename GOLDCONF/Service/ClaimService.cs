using GoldConf.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace GoldConf.Service
{
    public interface IClaimService
    {
        User GetLoggedUser();
        void SetHttpContext(HttpContext httpContext);
        void Logout();
        void Login(ClaimsPrincipal principal);

    }

    public class ClaimService : IClaimService
    {
        private IGoldConfContext context;
        private HttpContext httpContext;

        public ClaimService(IGoldConfContext context)
        {
            this.context = context;
        }

        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public User GetLoggedUser()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            var user = context.Users.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }

        public void Logout()
        {
            httpContext.SignOutAsync();
        }

        public void Login(ClaimsPrincipal principal)
        {
            httpContext.SignInAsync(principal);
        }

    }
}

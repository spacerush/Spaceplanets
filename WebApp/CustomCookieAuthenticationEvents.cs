using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using SpacePlanetsDAL.Services;

namespace WebApp
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IAuthenticationService _authenticationService;

        public CustomCookieAuthenticationEvents(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            userPrincipal
            return base.ValidatePrincipal(context);
        }
    }
}
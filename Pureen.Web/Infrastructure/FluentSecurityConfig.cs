using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using FluentSecurity;
using Pureen.Domain.Entities;
using Pureen.Web.Controllers;

namespace Pureen.Web.Infrastructure
{
    public static class FluentSecurityConfig
    {
        public static void Configure()
        {
            SecurityConfigurator.Configure(configuration =>
            {
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);

                var listaRoles = new List<string>();
                listaRoles.Add("Admin");
                listaRoles.Add("User");
                configuration.GetRolesFrom(() => 
                {   
                 var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (authCookie != null)
                    {
                        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        return authTicket.UserData.Split(',');
                    }
                    else
                    {
                        return new[]{""};
                    }
                });


                configuration.ForAllControllers().DenyAnonymousAccess();
                
                configuration.For<PublicController>(x => x.Index()).Ignore();
                configuration.For<AccountController>(x => x.Register()).Ignore();
                configuration.For<AccountController>(x => x.ProfilePermissions()).Ignore();
                configuration.For<AccountController>(x => x.JellieHi()).Ignore();
                configuration.For<AccountController>(x => x.ShowToolbar()).Ignore();
                configuration.For<AccountController>(x => x.Login()).Ignore();
                
                //configuration.For<AdministratorController>(x => x.Edit()).Ignore();
                
                configuration.For<AdministratorController>(x => x.AdminCp()).RequireRole(@"Admin");

               configuration.ResolveServicesUsing(type =>
                {
                    if (type == typeof(IPolicyViolationHandler))
                    {
                        var types = Assembly
                            .GetAssembly(typeof(MvcApplication))
                            .GetTypes()
                            .Where(x => typeof(IPolicyViolationHandler).IsAssignableFrom(x)).ToList();

                        var handlers = types.Select(t => Activator.CreateInstance(t) as IPolicyViolationHandler).ToList();

                        return handlers;
                    }
                    return Enumerable.Empty<object>();
                });
            });

        }
        
    }
}
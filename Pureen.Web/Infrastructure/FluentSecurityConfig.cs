using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using BootstrapMvcSample.Controllers;
using FluentSecurity;

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

                configuration.ForAllControllers().DenyAnonymousAccess();
                configuration.For<PublicController>(x => x.Index()).Ignore();
                configuration.For<AccountController>(x => x.Register()).Ignore();
                configuration.For<AccountController>(x => x.Login()).Ignore();
                configuration.For<AdministratorController>(x => x.AdminCp()).Ignore();

                /*configuration.For<HomeController>(x => x.Create()).RequireRole(new object[] { "Admin" });
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
                });*/
            });

        }
    }
}
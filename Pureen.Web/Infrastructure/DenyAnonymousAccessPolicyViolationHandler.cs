using System.Web.Mvc;
using System.Web.Routing;
using FluentSecurity;

namespace Pureen.Web.Infrastructure
{
    public class DenyAnonymousAccessPolicyViolationHandler : IPolicyViolationHandler
    {
        #region IPolicyViolationHandler Members

        public ActionResult Handle(PolicyViolationException exception)
        {
            return new RedirectToRouteResult("Login",
                                             new RouteValueDictionary
                                                 {{"error", "You have to be logged in order to view this website"}});
        }

        #endregion
    }
}
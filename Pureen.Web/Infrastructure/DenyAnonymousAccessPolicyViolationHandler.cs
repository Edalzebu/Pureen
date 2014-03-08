using System.IO;
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
             
             return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Public", action = "Index", area = "" }));
        }

        #endregion
    }

    public class AdministratorsOnlyPolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            return new HttpUnauthorizedResult(exception.Message);
        }
    }
}
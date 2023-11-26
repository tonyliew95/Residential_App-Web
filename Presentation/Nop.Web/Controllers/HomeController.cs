using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Security;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        [HttpsRequirement(SslRequirement.No)]
        public virtual IActionResult Index()
        {
            return View();
            //return Redirect(Url.Content("~/Admin")); //WilliamHo 20190117 MDT-680 custom redirect to CustomerIndex
            //return RedirectToAction("Login", "Customer"); //WilliamHo 20190117 MDT-680 
        }
    }
}
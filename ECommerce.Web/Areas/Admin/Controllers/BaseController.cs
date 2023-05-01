using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class BaseController : Controller
{
    protected ILifetimeScope _scope;
    public BaseController(ILifetimeScope scope)
    {
        _scope = scope;
    }
}

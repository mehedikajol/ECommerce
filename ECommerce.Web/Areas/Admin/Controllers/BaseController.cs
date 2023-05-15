using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers;

[Authorize, Area("Admin")]
public class BaseController : Controller
{
    protected ILifetimeScope _scope;
    public BaseController(ILifetimeScope scope)
    {
        _scope = scope;
    }
}

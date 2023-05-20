using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;

namespace ECommerce.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserProfileService _profileService;
    private readonly FileStorageSettings _settings;

    public ProfileController(
        IUserProfileService profileService,
        IOptions<FileStorageSettings> options)
    {
        _profileService = profileService;
        _settings = options.Value;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Orders()
    {
        return View();
    }

    public async Task<IActionResult> Details()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var profile = await _profileService.GetUserProfileByIdentityId(new Guid(userId));
        var model = new ProfileDetailsModel
        {
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Email = profile.Email,
            Phone = profile.Phone,
            Gender = Enum.GetName(typeof(Gender), profile.Gender),
            Address = profile.Address,
            ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, profile.ProfilePictureUrl)
        };
        return View(model);
    }
}
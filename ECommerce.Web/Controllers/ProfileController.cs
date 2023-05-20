using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> EditProfile()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var entity = await _profileService.GetUserProfileByIdentityId(new Guid(userId));
        var model = new ProfileEditModel
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email= entity.Email,
            Phone = entity.Phone,
            ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, entity.ProfilePictureUrl),
            Address = entity.Address,
            Gender = entity.Gender
        };

        var genders = from Gender s in Enum.GetValues(typeof(Gender))
                             select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["Genders"] = new SelectList(genders, "Id", "Name");

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(ProfileEditModel model)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var entity = await _profileService.GetUserProfileByIdentityId(new Guid(userId));

        var genders = from Gender s in Enum.GetValues(typeof(Gender))
                      select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["Genders"] = new SelectList(genders, "Id", "Name");

        return View(model);
    }

}
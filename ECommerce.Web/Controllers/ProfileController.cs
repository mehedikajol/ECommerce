using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using ECommerce.Web.Extensions;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Profile;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserProfileService _profileService;
    private readonly IOrderService _orderService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFileHandlerService _fileHandlerService;
    private readonly FileStorageSettings _settings;

    public ProfileController(
        IUserProfileService profileService,
        IOrderService orderService,
        ICurrentUserService currentUserService,
        IFileHandlerService fileHandlerService,
        IOptions<FileStorageSettings> options)
    {
        _profileService = profileService;
        _orderService = orderService;
        _currentUserService = currentUserService;
        _fileHandlerService = fileHandlerService;
        _settings = options.Value;
    }

    public async Task<IActionResult> Index()
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var profile = await _profileService.GetUserProfileByIdentityId(currentUserId);
        var model = new ProfileIndexModel();

        model.FullName = profile.FirstName + " " + profile.LastName;
        model.ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, profile.ProfilePictureUrl);

        model.TotalOrders = await _orderService.GetTotalOrderCountByUserIdAsync(currentUserId);
        model.PendingOrders = await _orderService.GetTotalPendingOrdersCountByUserIdAsync(currentUserId);
        model.TotalSpend = await _orderService.GetTotalSpendByUserIdAsync(currentUserId);
        model.TotalProducts = await _orderService.getTotalProductBoughtByUserIdAsync(currentUserId);

        return View(model);
    }

    public async Task<IActionResult> Orders()
    {
        //var orders = await 
        var currentUserId = _currentUserService.GetCurrentUserId();
        var orders = await _orderService.GetAllOrdersByUserId(currentUserId);
        var model = new ProfileOrdersListModel();

        foreach(var  order in orders)
        {
            var orderModel = order.Adapt<ProfileOrderModel>();
            orderModel.OrderStatusInWord = ((OrderStatus)orderModel.OrderStatus).ToString();

            model.Orders.Add(orderModel);
        }
        
        return View(model);
    }

    public async Task<IActionResult> Details()
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var profile = await _profileService.GetUserProfileByIdentityId(currentUserId);

        var model = profile.Adapt<ProfileDetailsModel>();
        model.Gender = ((Gender)profile.Gender).ToString();
        model.ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, profile.ProfilePictureUrl);        

        return View(model);
    }

    public async Task<IActionResult> Edit()
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var profile = await _profileService.GetUserProfileByIdentityId(currentUserId);

        var model = profile.Adapt<ProfileEditModel>();
        model.ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, profile.ProfilePictureUrl);

        var genders = from Gender s in Enum.GetValues(typeof(Gender))
                      select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["Genders"] = new SelectList(genders, "Id", "Name");

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProfileEditModel model)
    {
        var genders = from Gender s in Enum.GetValues(typeof(Gender))
                      select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["Genders"] = new SelectList(genders, "Id", "Name");

        if (ModelState.IsValid)
        {
            try
            {
                if (model.ImageFile is not null)
                {
                    var bytes = await model.ImageFile.ToByteArray();
                    var imageUrl = await _fileHandlerService.SaveFileAsync(bytes, model.ImageFile.FileName, UploadImageType.ProfileImage);
                    model.ProfilePictureUrl = imageUrl;
                    var oldEntity = await _profileService.GetUserProfileById(model.Id);
                    await _fileHandlerService.DeleteFileAsync(oldEntity.ProfilePictureUrl);
                }

                var profile = model.Adapt<UserProfile>();

                await _profileService.UpdateUserProfile(profile);
                return RedirectToAction(nameof(Details));
            }
            catch (NotFoundException)
            {
                return Redirect(url: "/Errors/Notfound");
            }
            catch (Exception)
            {
                return Redirect(url: "/Errors/InternalServerError");
            }
        }

        return View(model);
    }

}
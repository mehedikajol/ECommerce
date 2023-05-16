namespace ECommerce.Application.IServices;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
    string GetCurrentUserEmail();
}

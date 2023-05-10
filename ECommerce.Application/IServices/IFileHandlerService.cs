using ECommerce.Core.Enums;

namespace ECommerce.Application.IServices;

public interface IFileHandlerService
{
    Task<string> SaveFileAsync(byte[] fileBytes, string fileName, UploadImageType imageTypes = UploadImageType.Others);
    Task<(byte[] fileBytes, string fileExtension)> GetFileAsync(UploadImageType imageTypes, string fileName);
    Task DeleteFileAsync(string fileName);
}

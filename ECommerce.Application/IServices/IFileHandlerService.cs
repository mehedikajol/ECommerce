using ECommerce.Core.Enums;

namespace ECommerce.Application.IServices;

public interface IFileHandlerService
{
    Task<string> SaveFileAsync(byte[] fileBytes, string fileName, UploadImageTypes imageTypes = UploadImageTypes.others);
    Task<(byte[] fileBytes, string fileExtension)> GetFileAsync(UploadImageTypes imageTypes, string fileName);
    Task DeleteFileAsync(string fileName);
}

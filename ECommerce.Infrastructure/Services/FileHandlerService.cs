using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Services;

internal class FileHandlerService : IFileHandlerService
{
    private readonly FileStorageSettings _settings;

    public FileHandlerService(IOptions<FileStorageSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<(byte[] fileBytes, string fileExtension)> GetFileAsync(UploadImageTypes imageTypes, string fileName)
    {
        var baseDirectory = _settings.FileDirectory;
        var path = Path.Combine(baseDirectory, imageTypes.ToString(), fileName);
        var extension = Path.GetExtension(path);
        var bytes = await File.ReadAllBytesAsync(path);
        return (bytes, extension);
    }

    public async Task<string> SaveFileAsync(byte[] fileBytes, string fileName, UploadImageTypes imageTypes = UploadImageTypes.Others)
    {
        var baseDirectory = _settings.FileDirectory;
        var fileDirectory = Path.Combine(baseDirectory, imageTypes.ToString());
        if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);

        string fileId = Guid.NewGuid().ToString();
        var fileRelativeUrl = Path.Combine(imageTypes.ToString(), fileId + Path.GetExtension(fileName));

        var filePath = Path.Combine(baseDirectory, fileRelativeUrl);
        using var stream = File.Create(filePath);
        await stream.WriteAsync(fileBytes, 0, fileBytes.Length);
        return fileRelativeUrl;
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var baseFileDirectory = _settings.FileDirectory;
        var filePath = Path.Combine(baseFileDirectory, fileName);
        try
        {
            File.Delete(filePath);
        }
        catch (Exception) { }
    }
}

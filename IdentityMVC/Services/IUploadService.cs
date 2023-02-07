using Microsoft.AspNetCore.Mvc;

namespace IdentityMVC.Services
{
    public interface IUploadService
    {
        Task<string> UploadFile(IFormFile formFile);
    }

    public class UploadService : IUploadService
    {

        public async Task<string> UploadFile(IFormFile formFile)
        {
            string path;
            try
            {
                if (formFile.Length>0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads"));

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    string fname = Guid.NewGuid().ToString("N")+Path.GetExtension(formFile.FileName);

                    using (var fs = new FileStream(Path.Combine(path, fname), FileMode.Create))
                    {
                        await formFile.CopyToAsync(fs);
                    }

                    return $"{fname}";
                }
            }
            catch (Exception ex) { }
            return string.Empty;
        }
    }
}

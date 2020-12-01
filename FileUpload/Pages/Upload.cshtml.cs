using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FileUpload.Data;
using FileUpload.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;

namespace FileUpload.Pages
{
    [Authorize]
    public class UploadModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private int _size = 200;

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public IFormFile Upload { get; set; }

        public UploadModel(IWebHostEnvironment environment, ApplicationDbContext context, IConfiguration configuration)
        {
            _environment = environment;
            _context = context;
            _configuration = configuration;
            Int32.TryParse(_configuration["Thumbnail:Size"], out int _size);
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var fileRecord = new StoredFile {
                OriginalName = Upload.FileName,
                UploaderId = userId,
                Uploaded = DateTime.Now,
                ContentType = Upload.ContentType
            };
            string extension = System.IO.Path.GetExtension(Upload.FileName);
            if (Upload.ContentType.StartsWith("image"))
            {
                MemoryStream ims = new MemoryStream();
                MemoryStream oms = new MemoryStream();
                Upload.CopyTo(ims);
                IImageFormat format;
                using (Image image = Image.Load(ims.ToArray(), out format))
                {
                    int largestSize = Math.Max(image.Height, image.Width);
                    if (image.Width > image.Height)
                    {
                        image.Mutate(x => x.Resize(0, _size));
                    }
                    else
                    {
                        image.Mutate(x => x.Resize(_size, 0));
                    }
                    image.Mutate(x => x.Crop(new Rectangle((image.Width - _size) / 2, (image.Height - _size) / 2, _size, _size)));
                    image.Save(oms, format);
                }
                fileRecord.Thumbnail = oms.ToArray();
            }

            try
            {
                _context.Files.Add(fileRecord);
                await _context.SaveChangesAsync();

                var file = Path.Combine(_environment.ContentRootPath, "Uploads", fileRecord.Id.ToString());

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                    SuccessMessage = "File was uploaded succesfully.";
                }
            }
            catch
            {
                ErrorMessage = "File upload failed miserably.";
            }
            return RedirectToPage("/Index");
        }
    }
}

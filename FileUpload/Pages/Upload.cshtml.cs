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

namespace FileUpload.Pages
{
    [Authorize]
    public class UploadModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private ApplicationDbContext _context;

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public IFormFile Upload { get; set; }

        public UploadModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _environment = environment;
            _context = context;
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUpload.Pages
{
    public class UploadModel : PageModel
    {
        private IWebHostEnvironment _environment;

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public IFormFile Upload { get; set; }

        public UploadModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "Uploads", Upload.FileName);
            string extension = System.IO.Path.GetExtension(Upload.FileName);
            try
            {
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

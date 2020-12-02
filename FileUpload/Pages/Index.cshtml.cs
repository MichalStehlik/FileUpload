using FileUpload.Data;
using FileUpload.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FileUpload.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly ILogger<IndexModel> _logger;
        private ApplicationDbContext _context;

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public ICollection<StoredFile> Files { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment, ApplicationDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public void OnGet()
        {
            Files = _context.Files.AsNoTracking().Include(f => f.Uploader).ToList();
        }

        public IActionResult OnGetDownload(string filename)
        {
            var fullName = Path.Combine(_environment.ContentRootPath, "Uploads", filename);
            if (System.IO.File.Exists(fullName))
            {
                var fileRecord = _context.Files.Find(Guid.Parse(filename));
                if (fileRecord != null)
                {
                    return PhysicalFile(fullName, fileRecord.ContentType, fileRecord.OriginalName);
                }
                else
                {
                    ErrorMessage = "There is no record for such file.";
                    return RedirectToPage();
                }
            }
            else
            {
                ErrorMessage = "There is no such file.";
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnGetThumbnail(string filename)
        {
            StoredFile file = await _context.Files
              .AsNoTracking()
              .Where(f => f.Id == Guid.Parse(filename))
              .Include(f => f.Thumbnail)
              .SingleOrDefaultAsync();
            if (file != null)
            {
                if (file.Content != null)
                {
                    return File(file.Thumbnail.Blob,file.ContentType);
                }
                return NotFound("no thumbnail for this file");
            }
            return NotFound("no record for this file");
        }
    }
}

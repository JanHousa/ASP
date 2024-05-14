using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;
using UploadDownload.Data;

namespace UploadDownload.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UploadModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile != null)
            {
                using var memoryStream = new MemoryStream();
                await UploadedFile.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using var image = Image.Load(memoryStream);
                image.Mutate(x => x.AutoOrient());

                if (image.Width * image.Height > 2000000)
                {
                    var factor = Math.Sqrt(2000000 / (double)(image.Width * image.Height));
                    image.Mutate(x => x.Resize((int)(image.Width * factor), (int)(image.Height * factor)));
                }

                memoryStream.SetLength(0);
                image.SaveAsJpeg(memoryStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 90 });
                memoryStream.Position = 0;

                var file = new StoredFile
                {
                    Id = Guid.NewGuid(),
                    OriginalName = UploadedFile.FileName,
                    ContentType = UploadedFile.ContentType,
                    UploadedAt = DateTime.UtcNow,
                    Data = memoryStream.ToArray()
                };

                _context.StoredFiles.Add(file);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");

            }

            return Page();
        }
    }
}

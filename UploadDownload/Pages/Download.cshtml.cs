using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using UploadDownload.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace UploadDownload.Pages
{
    public class DownloadModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DownloadModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid fileId)
        {
            
            var file = await _context.StoredFiles
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file == null)
            {
                return NotFound();
            }

            return File(file.Data, file.ContentType, file.OriginalName);
        }
    }
}

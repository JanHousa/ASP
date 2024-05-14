using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadDownload.Data;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UploadDownload.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<StoredFile> Files { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Files = await _context.StoredFiles
                                   .AsNoTracking()
                                   .ToListAsync();
        }
    }
}

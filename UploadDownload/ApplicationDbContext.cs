using Microsoft.EntityFrameworkCore;

namespace UploadDownload.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<StoredFile> StoredFiles { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using UploadImageApi.Models.Domain;

namespace UploadImageApi.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }
       
        public DbSet<Image>images { get; set; }
    }
}

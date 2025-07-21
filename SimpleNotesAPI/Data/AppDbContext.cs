using Microsoft.EntityFrameworkCore;
using SimpleNotesAPI.Model;

namespace SimpleNotesAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Note> Notes { get; set; }
    }
}

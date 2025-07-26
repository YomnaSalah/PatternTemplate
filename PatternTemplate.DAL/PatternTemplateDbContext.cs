using Microsoft.EntityFrameworkCore;

namespace PatternTemplate.DAL
{
    public class PatternTemplateDbContext : DbContext
    {
        public PatternTemplateDbContext(DbContextOptions<PatternTemplateDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;
using RestServerlessFuncApp.Core.Entities;

namespace RestServerlessFuncApp.Infra
{
    public class AppDataContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public AppDataContext(DbContextOptions options)
            : base(options)
        {
                
        }
    }
}


using Microsoft.Data.Entity;
using Yotodo.Models;
 
namespace Yotodo.Data
{ 
    public class ApplicationDataContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<Todo>()
                .ToTable("todo")
                .HasKey(x => x.Id)
                ;
           
        }
    }
}
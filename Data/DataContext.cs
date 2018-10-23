using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class DataContext : DbContext
    {
        // Properties
        public DbSet<Profile> Profiles { get; set; }

        // Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
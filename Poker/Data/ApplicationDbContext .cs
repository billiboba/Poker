using Microsoft.EntityFrameworkCore;
using Poker.Models;
using System.Collections.Generic;

namespace Poker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}

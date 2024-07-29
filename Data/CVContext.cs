using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_2023.Models;

namespace Project_2023.Data
{
    public class CVContext : DbContext
    {
        public CVContext (DbContextOptions<CVContext> options)
            : base(options)
        {
        }

        public DbSet<Models.CV> CV { get; set; } = default!;
        public DbSet<Models.Skill> Skills { get; set; }
        
        public DbSet<Models.HasSkills> HasSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CV>().ToTable("CV");
            modelBuilder.Entity<Skill>().ToTable("Skill");
            modelBuilder.Entity<HasSkills>().ToTable("HasSkills");
        }
    }
}

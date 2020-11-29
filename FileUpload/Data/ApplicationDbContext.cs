using FileUpload.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUpload.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<StoredFile> Files { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "3d2d8122-1111-4f47-9666-a7e5fde03bca",
                Email = "st@pslib.cloud",
                NormalizedEmail = "ST@PSLIB.CLOUD",
                EmailConfirmed = true,
                LockoutEnabled = false,
                UserName = "st@pslib.cloud",
                NormalizedUserName = "ST@PSLIB.CLOUD",
                PasswordHash = hasher.HashPassword(null, "Admin_1234"),
                SecurityStamp = string.Empty
            });
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Entities;

namespace Ultatel.Data.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Student>()
               .HasOne<ApplicationUser>(s => s.AddedByUser)
               .WithMany(u => u.Students) 
               .HasForeignKey(s => s.AddedByUserId)
               .IsRequired();

         builder.Entity<ApplicationUser>()
        .Property(u => u.UserName)
        .IsRequired()                               // UserName is required
        .HasMaxLength(256)                          // Maximum length for UserName
        .HasAnnotation("RegularExpression", @"^[a-zA-Z0-9 ]*$")  // Regex allowing alphanumeric and spaces
        .HasAnnotation("InvalidUserName", "Username '{0}' is invalid, can only contain letters, digits, or spaces.");
        }
    }
    }



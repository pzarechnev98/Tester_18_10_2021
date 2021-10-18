using Microsoft.EntityFrameworkCore;
using Tester.Models.Entities;

namespace Tester.Models
{
    public class TestContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Common> Commons { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<CenterUser> CnterUsers { get; set; }
        public DbSet<City> Citites { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CenterUser>()
                .HasKey(bc => new { bc.UserId, bc.CenterId });
            modelBuilder.Entity<CenterUser>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.CenterUsers)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<CenterUser>()
                .HasOne(bc => bc.Center)
                .WithMany(c => c.CenterUsers)
                .HasForeignKey(bc => bc.CenterId);


            // ----------   30/09/2021   -------------
            modelBuilder.Entity<UserLanguage>()
                .HasKey(bc => new { bc.UserId, bc.LanguageId });
            modelBuilder.Entity<UserLanguage>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserLanguages)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserLanguage>()
                .HasOne(bc => bc.Language)
                .WithMany(c => c.UserLanguages)
                .HasForeignKey(bc => bc.LanguageId);

            modelBuilder.Entity<StudentGroup>()
                .HasKey(bc => new { bc.StudentId, bc.GroupId });
            modelBuilder.Entity<StudentGroup>()
                .HasOne(bc => bc.Student)
                .WithMany(b => b.StudentGroups)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<StudentGroup>()
                .HasOne(bc => bc.Group)
                .WithMany(c => c.StudentGroups)
                .HasForeignKey(bc => bc.GroupId);
            // ----------   30/09/2021   -------------


            // ----------   04.10.2021   -------------
            modelBuilder.Entity<CenterUser>()
                .HasKey(bc => new { bc.UserId, bc.CenterId });
            modelBuilder.Entity<CenterUser>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.CenterUsers)
                .HasForeignKey(bc => bc.CenterId);
            modelBuilder.Entity<CenterUser>()
                .HasOne(bc => bc.Center)
                .WithMany(c => c.CenterUsers)
                .HasForeignKey(bc => bc.CenterId);


            // ----------   04.10.2021   -------------


        }



    }
}

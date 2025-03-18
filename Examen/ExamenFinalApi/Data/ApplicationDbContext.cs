using ExamenFinalApi.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamenFinalApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add a sample object
            //modelBuilder.Entity<ObjetoEntity>().HasData(
            //    new ObjetoEntity
            //    {
            //        Id = 1,
            //        Name = "Objeto de Prueba",
            //        BoolOption = true,
            //        CreatedDate = DateTime.UtcNow
            //    }
            //);

        }
        //Add models here
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ObjetoEntity> Objeto { get; set; }
        public DbSet<ObjetoDosEntity> ObjetoDos { get; set; }
        public DbSet<ObjetoTresEntity> ObjetoTres { get; set; }

    }
}

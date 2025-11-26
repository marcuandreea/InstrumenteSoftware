using CoffeeShop.data.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Abonament> Abonament { get; set; }
        public DbSet<Cos_cumparaturi> Cos_cumparaturi { get; set; }
        public DbSet<Espressoare> Espressoare { get; set; }
        public DbSet<Recenzii> Recenzii { get; set; }
        public DbSet<Tipuri_Cafea> Tipuri_Cafea { get; set; }
    }
}
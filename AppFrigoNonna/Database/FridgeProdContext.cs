using AppFrigoNonna.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppFrigoNonna.Database
{
    public class FridgeProdContext : DbContext
    // Poi per le auth, sostituire DbContext con IdentityDbContext<IdentityUser>
    // Aggiungere a program.cs app.UseAuthentication();

    {
        public DbSet<FridgeProd> FridgeProds { get; set; }

        public DbSet<Category> Categories { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=FrigoDB; TrustServerCertificate=True; Integrated Security=True");
        }
        // QUESTO METODO ^^^^^^^^^ IMPOSTA LA STRINGA DI CONNESSIONE importante aggiungere:  TrustServerCertificate=True;
    }
}

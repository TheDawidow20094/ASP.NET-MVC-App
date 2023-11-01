using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence
{
    public class CarWorkshopDbContext : IdentityDbContext
    {
        //private const string _connString = "Server=(localdb)\\mssqllocaldb;Database=CarWorkshopDb;Trusted_Connection=True;";        
        public DbSet<CarWorkshop.Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
        public DbSet<CarWorkshop.Domain.Entities.CarWorkshopService> Services { get; set; }

        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Nie utworzy nowej tabeli dla CarWorkshopContactDetails, inaczej bym musiał dodać primary key w tej klasie
            modelBuilder.Entity<CarWorkshop.Domain.Entities.CarWorkshop>().OwnsOne(c => c.ContactDetails);

            //Konfiguracja relacji między tabelą CarWorkshop oraz CarWorkspoServices
            modelBuilder.Entity<CarWorkshop.Domain.Entities.CarWorkshop>().HasMany(c => c.Services)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);
        }
    }
}

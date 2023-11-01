using CarWorkshop.Infrastructure.Persistence;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (_dbContext.CarWorkshops.Any() == false) //Sprawdzenie czy sa jakieś dane
                {
                    var mazdaAso = new Domain.Entities.CarWorkshop()
                    {
                        Name = "Mazda ASO",
                        Description = "Authorized Mazda service",
                        About = "Best Mazda service",
                        ContactDetails = new()
                        {
                            City = "Poznań",
                            Street = "Szwedzka 2",
                            PostalCode = "52-255",
                            PhoneNo = "+48505222999",
                        }
                    };
                    mazdaAso.EncodeName();
                    _dbContext.CarWorkshops.Add(mazdaAso);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}

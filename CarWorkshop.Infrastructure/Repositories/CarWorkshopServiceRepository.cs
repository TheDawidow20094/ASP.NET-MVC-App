using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
    {
        readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopServiceRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateService(CarWorkshopService carWorkshopService)
        {
            _dbContext.Services.Add(carWorkshopService);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarWorkshopService>> GetAllCarWorkshopServicesByEncodedName(string encodedName)
        {
            return await _dbContext.Services.Where(s => s.CarWorkshop.EncodedName == encodedName).ToListAsync();
        }
    }
}

using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopRepository : ICarWorkshopRepository
    {
        private CarWorkshopDbContext _dbContext;

        public CarWorkshopRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Commit()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task CreateWorkshop(Domain.Entities.CarWorkshop carWorkshop)
        {
            _dbContext.Add(carWorkshop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAllWorkshops()
        {
            return await _dbContext.CarWorkshops.ToListAsync();
        }

        public async Task<Domain.Entities.CarWorkshop> GetWorkshopByEncodedName(string encodedName)
        {
            return await _dbContext.CarWorkshops.FirstAsync(i => i.EncodedName == encodedName);
        }

        public Task<Domain.Entities.CarWorkshop?> GetWorkshopByName(string workshopName)
        {
            return _dbContext.CarWorkshops.FirstOrDefaultAsync(i => i.Name.ToLower() == workshopName.ToLower());
        }
    }
}

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopRepository
    {
        Task CreateWorkshop(Domain.Entities.CarWorkshop carWorkshop);
        Task<Domain.Entities.CarWorkshop?> GetWorkshopByName(string workshopName);
        Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAllWorkshops();
        Task<Domain.Entities.CarWorkshop> GetWorkshopByEncodedName(string encodedName);
        Task Commit();
    }
}

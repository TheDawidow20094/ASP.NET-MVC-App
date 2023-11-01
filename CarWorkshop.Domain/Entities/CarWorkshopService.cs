using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshopService
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;

        //Referencja do klucza głównego
        public int CarWorkshopId { get; set; } = default!;
        public CarWorkshop CarWorkshop { get; set; } = default!;


    }
}

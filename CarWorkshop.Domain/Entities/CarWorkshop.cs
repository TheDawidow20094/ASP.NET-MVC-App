﻿using Microsoft.AspNetCore.Identity;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CarWorkshopContactDetails ContactDetails { get; set; } = default!;
        public string EncodedName { get; private set; } = default!;
        public string? About { get; set; }
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }

        //Referencja dla podtabeli CarWorkshopService
        public List<CarWorkshopService> Services { get; set; } = new List<CarWorkshopService>();


        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}

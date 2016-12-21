using System;

namespace PSI.WebAPI.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public DateTime RegistryDate { get; set; }
    }
}
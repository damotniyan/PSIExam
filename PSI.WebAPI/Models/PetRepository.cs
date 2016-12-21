using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace PSI.WebAPI.Models
{
    public class PetRepository
    {
        /// <summary>
        /// Creates a new pet with default values
        /// </summary>
        /// <returns></returns>
        internal Pet Create()
        {
            Pet pet = new Pet
            {
                RegistryDate = DateTime.Now
            };
            return pet;
        }

        /// <summary>
        /// Retrieves the list of pets.
        /// </summary>
        /// <returns></returns>
        internal List<Pet> Retrieve()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/pet.json");

            var json = System.IO.File.ReadAllText(filePath);

            var pets = JsonConvert.DeserializeObject<List<Pet>>(json);

            return pets;
        }

        /// <summary>
        /// Saves a new pet.
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        internal Pet Save(Pet pet)
        {
            // Read in the existing pets
            var pets = this.Retrieve();

            // Assign a new Id
            var maxId = pets.Max(p => p.PetId);
            pet.PetId = maxId + 1;
            pets.Add(pet);

            WriteData(pets);
            return pet;
        }

        /// <summary>
        /// Updates an existing pet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pet"></param>
        /// <returns></returns>
        internal Pet Save(int id, Pet pet)
        {
            // Read in the existing pets
            var pets = this.Retrieve();

            // Locate and replace the item
            var itemIndex = pets.FindIndex(p => p.PetId == pet.PetId);
            if (itemIndex > 0)
            {
                pets[itemIndex] = pet;
            }
            else
            {
                return null;
            }

            WriteData(pets);
            return pet;
        }


        private bool WriteData(List<Pet> pets)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/pet.json");

            var json = JsonConvert.SerializeObject(pets, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }

    }
}
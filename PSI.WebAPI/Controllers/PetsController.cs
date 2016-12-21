using PSI.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PSI.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:22205", "*", "*")]
    public class PetsController : ApiController
    {
        // GET: api/Pets
        public IEnumerable<Pet> Get()
        {
            var petRepository = new PetRepository();
            return petRepository.Retrieve();
        }

        // GET: api/Pets/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pets
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pets/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pets/5
        public void Delete(int id)
        {
        }
    }
}

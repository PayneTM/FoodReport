using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodReport.DAL.Models;
using FoodReport.DAL.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodReport.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _noteRepository;

        public ProductController(IRepository<Product> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Product>> Get()
        {
            return GetProductInternal();
        }

        private async Task<IEnumerable<Product>> GetProductInternal()
        {
            return await _noteRepository.GetAll();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public Task<Product> Get(string id)
        {
            return GetNoteByIdInternal(id);
        }

        private async Task<Product> GetNoteByIdInternal(string id)
        {
            return await _noteRepository.Get(id) ?? new Product();
        }

        // POST api/notes
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //    _noteRepository.Add(new Product()
        //    {
        //        Body = value,
        //        CreatedOn = DateTime.Now,
        //        UpdatedOn = DateTime.Now
        //    });
        //}

        // PUT api/notes/5
        //[HttpPut("{id}")]
        //public void Put(string id, [FromBody]string value)
        //{
        //    _noteRepository.Update(id, value);
        //}

        // DELETE api/notes/5
        public void Delete(string id)
        {
            _noteRepository.Remove(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using blog.Domain;
using blog.Model;
using blog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blog.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {            
            var  categoy = await _repository.GetAsync(id);                
          
            if(categoy == null)
            {
                return NotFound();
            }

            var result = new CategoryModel()
            {
                Id = categoy.Id,
                Name = categoy.Name,
                Description = categoy.Description
            };

            return Ok(result);
        }

        [HttpGet]
        public  async Task<IActionResult>  GetAll()
        {
            var listCategorys = await  _repository.Query().ToListAsync();
            return Ok(listCategorys);
        }        
    
        
        /// Post Categories
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CategoryModel model)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = new Category()
            {
                Name = model.Name,
                Description = model.Description
            };

            await _repository.InsertAsync(newCategory);

            return Created($"categorys/{newCategory.Id}", newCategory);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CategoryModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoy =  await _repository.GetAsync(id);

            if(categoy == null )  
            {
                return NotFound();
            }

            categoy.Name = model.Name;
            categoy.Description = model.Description;
            categoy.Updated_At = DateTime.Now;

            await _repository.UpdateAsync(categoy);

            return Ok(categoy);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var category = _repository.GetAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return Ok();
        }
        
    }
}
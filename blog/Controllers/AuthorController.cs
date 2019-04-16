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
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get (int id)
        {
            var author = await _repository.GetAsync(id);

            if(author == null)
            {
                return NotFound();
            }   

            var result = new AuthorModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                Media = author.Media.Name
            };
            
            return Ok(result);
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> Search(string search )
        {
            var author = await _repository.Query().Where(a => a.Name.Contains(search)).FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }

            var result = new AuthorModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                Media = author.Media.Path
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthorModel model)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var author = new Author()
            {
                Description = model.Description,
                Name = model.Name
            };            

            await _repository.InsertAsync(author);

            return Ok(author);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();                
            }

            var result = await _repository.GetAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            result.Name = model.Name;
            result.Description = model.Description;

            await _repository.UpdateAsync(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.GetAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return Ok();
        }         
    }
}
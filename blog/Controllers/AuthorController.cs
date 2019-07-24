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
        public async Task<IActionResult> Get(int id)
        {
            var author = await _repository.GetAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var result = new AuthorModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                Media = string.Empty
            };

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string search)
        {
            var author = await _repository.Query().Where(a => a.Name.ToLower().Contains(search.ToLower())).ToListAsync();

            if (author == null)
            {
                return NotFound();
            }

            var result = new AuthorListModel
            {
                Authors = author.Select(a => new AuthorModel
                {                                   
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Media = a.Media.Path
                })
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthorModel model)
        {
            
            if (!ModelState.IsValid || model == null )
            {
                return BadRequest();
            }

            var author = new Author()
            {
                Description = model.Description,
                Name = model.Name
            };

            await _repository.InsertAsync(author);

            return Created($"author/{author.Id}", author);
            
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _repository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            result.Name = model.Name;
            result.Description = model.Description;

            await _repository.UpdateAsync(result);

            return Accepted(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            
            return Ok();
        }
    }
}
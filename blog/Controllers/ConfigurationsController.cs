using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Repositories;
using blog.Model;
using blog.Domain;

namespace blog.Controllers
{
    [Route("[controller]")]
    public class ConfigurationsController : Controller
    {
        private IConfigurationRepository _configurationRepository;

        public ConfigurationsController(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var configuration = await _configurationRepository.GetAsync(id);

            if (configuration != null)
            {
                return Ok(configuration);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Configuration configuration = new Configuration()
            {
                Head = model.Head,
                Body = model.Body,
                Footer = model.Footer,
                ArticleId = model.ArticleId
            };

            await _configurationRepository.InsertAsync(configuration);
            return Ok(configuration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ConfigurationModel model)
        {
            var configuration = await _configurationRepository.GetAsync(id);

            if (!ModelState.IsValid || configuration == null)            
                return BadRequest();

            configuration.Head = model.Head;
            configuration.Body = model.Body;
            configuration.Footer = model.Footer;
            
            return Ok(configuration);               
            
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var configuration = await _configurationRepository.GetAsync(id);
            
            if(configuration == null)
            return NotFound();

            await _configurationRepository.DeleteAsync(id);
            return Ok();
        }

    }
}
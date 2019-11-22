using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using blog.Domain;
using blog.Model;
using blog.Infraestructure;
using blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace blog.Controllers
{
    [Route("[controller]")]
    public class MediaController : Controller
    {

        private IMediaRepository _mediaRepository;
        private IArticleRepository _articleRepository;
        private IHostingEnvironment _hostingEnvironment;
        private IMediaArticleRespository _mediaArticleRepository;
        private Random rndNumber;


        public MediaController(
            IMediaRepository mediaRepository,
            IArticleRepository articleRepository,
            IHostingEnvironment environment,
            IMediaArticleRespository mediaArticleRespository)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            if (string.IsNullOrEmpty(environment.WebRootPath))
                environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory());

            _hostingEnvironment = environment;
            _mediaRepository = mediaRepository;
            _articleRepository = articleRepository;
            _hostingEnvironment = environment;
            _mediaArticleRepository = mediaArticleRespository;
            rndNumber = new Random();

        }



        [HttpPost("{idArticle}/UploadMediaArticle")]
        public async Task<IActionResult> PostMediaArticle(int idArticle, List<IFormFile> files)
        {

            long size = files.Sum(f => f.Length);
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");


            foreach (var file in files)
            {

                if (file.Length > 0)
                {
                    MediaArticle mediaArticle = new MediaArticle();
                    mediaArticle.Article = await _articleRepository.GetAsync(idArticle);


                    if (mediaArticle.Article != null)
                    {
                        string fileName = $"{rndNumber.Next()}-{idArticle}-{file.FileName}";

                        Media newMedia = new Media() { Name = fileName, Path = uploads };

                        await _mediaRepository.InsertAsync(newMedia);

                        mediaArticle.Media = newMedia;

                        await _mediaArticleRepository.InsertAsync(mediaArticle);

                        var filePath = Path.Combine(uploads, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }

            return Ok();

        }

        [HttpPost("{idAuthor}/UploadMediaAuthor")]
        public async Task<IActionResult> PostMediaAuthor(int idAuthor, IFormFile file)
        {

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            if (file != null)
            {
                string fileName = $"{rndNumber.Next()}-{idAuthor}-{file.FileName}";
                var filePath = Path.Combine(uploads, fileName);
                Media newMedia = new Media() { Name = fileName, Path = uploads };

                await _mediaRepository.InsertAsync(newMedia);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(newMedia);
            }
            else
            {
                return BadRequest();
            }
        }




        [HttpGet("{idArticle}/GetMediasArticles")]
        public async Task<IActionResult> GetMediasArticles(int idArticle)
        {
            if (idArticle > 0)
            {

                var medias = _mediaArticleRepository.Query().Where(x => x.Article.Id == idArticle).Select(y => y.Media).ToList();
                if (medias.Count() > 0)
                {

                    return Ok(medias);
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("{idMedia}/GetMediaAuthor")]
        public async Task<IActionResult> GetMediaAuthor(int idMedia)
        {

            if (idMedia > 0)
            {
                var media = await _mediaRepository.GetAsync(idMedia);
                return Ok(media);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("idMedia")]
        public async Task<IActionResult> Delete(int idMedia)
        {
            var media = await _mediaRepository.GetAsync(idMedia);

            if (media != null)
            {
                var mediaArticle = _mediaArticleRepository.Query().Where(x => x.Media.Id == idMedia).FirstOrDefault();
                if (mediaArticle != null)
                    await _mediaArticleRepository.DeleteAsync(mediaArticle.Id);


                await _mediaRepository.DeleteAsync(idMedia);
                System.IO.File.Delete($"{media.Path}/{media.Name}");
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
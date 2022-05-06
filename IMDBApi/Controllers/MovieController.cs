using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LocalModel.Models;
using LocalModel.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Controler spécifique au objet Movie
    /// </summary>
    /// <remarks>Action de crud</remarks>
    
    
    public class MovieController : ControllerBase
    {
        private IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        /// <summary>
        /// Récupération de la liste complète de Movie
        /// </summary>
        /// <response code="200">Renvoi la liste des Movie</response>
        /// <response code="400">Une erreur côté serveur</response>
        /// <returns>IEnumerable de movie avec liste des acteurs, scénariste et réal</returns>
        /// <remarks>Accessible à tout le monde</remarks>
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_movieService.GetAll());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retourne un film sur base de son Id
        /// Parametre de type entier
        /// </summary>
        /// <response code="200">Renvoi un Movie</response>
        /// <response code="400">Une erreur côté serveur</response>
        /// <returns>Objet de type movie</returns>
        /// <remarks>Accessible aux Users connecté</remarks>
        [Authorize("User")]
        
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            return Ok(_movieService.GetOne(Id));
        }

        /// <summary>
        /// Permet d'enregistrer un Movie
        /// Nécéssite un model MovieToDal
        /// </summary>
        /// <response code="200">Création ok</response>
        /// <response code="400">Une erreur côté serveur</response>
        /// <returns>Un message en cas d'erreur</returns>
        /// <remarks>Accessible au role Admin</remarks>
        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Post([FromBody]NewMovie m)
        {
            try
            {
                int test = _movieService.Create(m);
                return Ok(test);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        /// <summary>
        /// Permet de mettre à jour un Movie sur base de son ID
        /// Nécéssite un model MovieToDal
        /// </summary>
        /// <response code="200">Mise à jour ok</response>
        /// <response code="400">Une erreur côté serveur</response>
        /// <returns>Un message en cas d'erreur</returns>
        /// <remarks>Accessible au role Admin</remarks>
        [Authorize("Admin")]
        [HttpPut]
        public IActionResult Put([FromBody]NewMovie m)
        {
            try
            {
                _movieService.Update(m);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer un Movie sur base de son ID
        /// </summary>
        /// <response code="200">Suppression ok</response>
        /// <response code="400">Une erreur côté serveur</response>
        /// <returns>Un message en cas d'erreur</returns>
        /// <remarks>Accessible au role Admin</remarks>
        [Authorize("Admin")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _movieService.Delete(Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[AllowAnonymous]
        //[HttpPost("upfile")]
        //public async Task<IActionResult> UploadImage()
        //{
            
        //    HttpRequest req = HttpContext.Request;
        //    string imageName = req.Form.Files[0].FileName;
        //    if (!Directory.Exists("test")) Directory.CreateDirectory("test"); 

        //    using (FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "image/", imageName), FileMode.Create))
        //    {
        //        await req.Form.Files[0].CopyToAsync(fs);


        //        return this.StatusCode(200, "http://localhost:53448/image/"+req.Form.Files[0].FileName);
        //    }

        //}
    }
}
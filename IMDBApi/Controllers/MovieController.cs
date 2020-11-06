using System;
using System.Collections.Generic;
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
        /// <response code="200">Renvoi la liste complète des Movie</response>
        /// <response code="400">Une erreur côté serveur</response>
        /// <returns></returns>
        
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

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            return Ok(_movieService.GetOne(Id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]MovieToDal m)
        {
            try
            {
                _movieService.Create(m);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut]
        public IActionResult Put([FromBody]MovieToDal m)
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
    }
}
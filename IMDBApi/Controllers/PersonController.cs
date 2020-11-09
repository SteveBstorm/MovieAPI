using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDBApi.Models;
using IMDBApi.Tools;
using LocalModel.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.GetAll());
        }

        [Authorize("User")]
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            return Ok(_personService.GetComplete(Id));
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Post(Person p)
        {
            _personService.Create(p.toLocal());
            return Ok();
        }

        [Authorize("Admin")]
        [HttpPost("setActor")]
        public IActionResult Post(Actor a)
        {
            _personService.SetAsActor(a.MovieId, a.PersonId, a.Role);
            return Ok();
        }
    }
}
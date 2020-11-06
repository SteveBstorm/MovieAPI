using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalModel.Models;
using IMDBApi.Models;
using LocalModel.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //private IMovieService _movieService;
        //private IPersonService _personService;
        //public TestController(IMovieService movieService, IPersonService personService)
        //{
        //    _movieService = movieService;
        //    _personService = personService;
        //}

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    Person real = new Person
        //    {
        //        Id = 1,
        //        LastName = "truc",
        //        FirstName = "toto"
        //    };

        //    Person scen = new Person
        //    {
        //        Id = 2,
        //        LastName = "truc",
        //        FirstName = "toto"
        //    };
        //    TestModel tm = new TestModel(real, scen, 4, "La colère des noob");

        //    return Ok(_movieService.GetAll());
        //}
    }
}
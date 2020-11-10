using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using LocalModel.Models;
using LocalModel.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        ICommentService<Comment> _service;
        public CommentController(ICommentService<Comment> service)
        {
            _service = service;
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            return Ok(_service.GetByMovieId(Id));
        }

        [HttpPost]
        public IActionResult Post(Comment c)
        {
            _service.Insert(c);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _service.Delete(Id);
            return Ok();
        }
    }
}
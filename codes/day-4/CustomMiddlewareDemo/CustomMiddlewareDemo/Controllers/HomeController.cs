using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomMiddlewareDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        //api/home/hello/john
        [Route("hello/{name?}")]
        public string Hello(string? name)
        {
            return $"Hello, {name}!";
        }

        [HttpGet]
        //api/home/get?x=5
        [Route("get")]
        public string Welcome([FromQuery(Name ="x")]int id)
        {
            return $"Hello, {id}!";
        }
    }
}

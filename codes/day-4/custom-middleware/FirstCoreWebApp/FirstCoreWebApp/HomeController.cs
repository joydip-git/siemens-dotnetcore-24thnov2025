using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstCoreWebApp
{
    [Route("[controller]")]
    [ApiController]    
    public class HomeController : ControllerBase
    {
        private readonly IRepo _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRepo repo, IConfiguration configuration, ILogger<HomeController> logger)
        {
            _repo = repo;
            _config = configuration;
            _logger = logger;
            Console.WriteLine("created....");
        }

        [HttpGet]
        [Route("hello")]
        public string Welcome()
        {
            _logger.LogInformation(_config.ToString());
            return _repo.GetData();
        }

        [HttpGet]
        [Route("get")]
        public string Get()
        {
            _logger.LogInformation(_config.ToString());
            return _repo.GetData();
        }
    }
}

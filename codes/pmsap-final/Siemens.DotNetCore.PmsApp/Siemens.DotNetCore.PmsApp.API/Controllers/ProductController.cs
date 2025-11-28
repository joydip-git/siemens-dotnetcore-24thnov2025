using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siemens.DotNetCore.PmsApp.ServiceManager;
using Siemens.DotNetCore.PmsApp.DTOs;

namespace Siemens.DotNetCore.PmsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IAsyncServiceManager<ProductDTO,int> _manager, ILogger<ProductController> _logger) : ControllerBase
    {
    }
}

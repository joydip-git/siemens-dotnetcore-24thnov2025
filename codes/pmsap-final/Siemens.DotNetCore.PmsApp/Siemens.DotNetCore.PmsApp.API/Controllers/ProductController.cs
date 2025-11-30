using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Siemens.DotNetCore.PmsApp.DTOs;
using Siemens.DotNetCore.PmsApp.ServiceManager;

namespace Siemens.DotNetCore.PmsApp.API.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController(IAsyncServiceManager<ProductDTO, int> _manager, ILogger<ProductController> _logger) : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<ProductDTO> products = await _manager.GetAllAsync();
                OkObjectResult okRes = this.Ok(products);
                return okRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all products.");
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                ProductDTO product = await _manager.GetAsync(id);
                OkObjectResult okRes = this.Ok(product);
                return okRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all products.");
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO dto)
        {
            try
            {
                ProductDTO product = await _manager.AddAsync(dto);
                CreatedAtActionResult createdRes = this.CreatedAtAction($"{nameof(AddProduct)}", product);
                return createdRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all products.");
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("edit")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromQuery(Name = "id")] int id, [FromBody] ProductDTO dto)
        {
            try
            {
                ProductDTO product = await _manager.UpdateAsync(id, dto);
                AcceptedAtActionResult updatedRes = this.AcceptedAtAction($"{nameof(UpdateProduct)}", product);
                return updatedRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all products.");
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{x}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct([FromRoute(Name = "x")] int id)
        {
            try
            {
                ProductDTO product = await _manager.RemoveAsync(id);
                OkObjectResult deleteRes = this.Ok(product);
                return deleteRes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all products.");
                return this.BadRequest(ex.Message);
            }
        }
    }
}

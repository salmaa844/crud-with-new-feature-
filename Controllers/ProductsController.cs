using crud_api2.Dtos.Products;
using crud2_newfeature.Dtos.Products;
using crudAPI.Data;
using crudAPI.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace crud2_newfeature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(ApplicationDbContext dbContext,ILogger<ProductsController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await dbContext.Products.Select(x => new GetProducts
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return Ok(products);
        }
        [HttpGet("Details")]
        public async Task<IActionResult> GetId(int id)
        {

            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                NotFound();
            }
            var productDto = product.Adapt<GetProductDto>();
            return Ok(productDto);
           
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDepatmentAcync(
            CreateProductDto productDto,
            [FromServices] IValidator<CreateProductDto> validator)
        {
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                var modelState = new ModelStateDictionary();
                validationResult.Errors.ForEach(error =>
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                });

                return ValidationProblem(modelState);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(int id,UpdateProductDto request)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            request.Adapt(product);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
		private readonly AppDbContextMongo _dbContext;

        public ProductsController(ILogger<ProductsController> logger, AppDbContextMongo dbContext)
        {
            _logger = logger;
			_dbContext = dbContext;
        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAll()
		{
			var products = await _dbContext.Products.ToListAsync();
			_logger.LogInformation($"Retrieved {products.Count} products");
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(Guid id)
		{
			var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
			if (product == null)
			{
				return NotFound(new { message = "Product not found" });
			}
			return Ok(product);
		}

		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(ProductDto product)
		{
			var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
			if (existingProduct != null)
			{
				return BadRequest(new { message = "Product already exists" });
			}

			var newProduct = new Product
			{
				Id = Guid.NewGuid(),
				Description = product.Description,
				Name = product.Name,
				Price = product.Price
			};
			await _dbContext.Products.AddAsync(newProduct);
			await _dbContext.SaveChangesAsync();
			return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
		}
    }
}
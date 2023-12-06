using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Context;
using Web.Dtos;
using Web.Entities;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly MyDbContext _db;

    public ProductController(MyDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        return await _db.Products.ToListAsync();
    }
    
    [AllowAnonymous]
    [HttpGet("GetMessage")]
    public string GetMessage()
    {
        return "Hello api";
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Insert(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return Created("", product);
    }
}
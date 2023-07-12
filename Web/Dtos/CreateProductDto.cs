using System.ComponentModel.DataAnnotations;

namespace Web.Dtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public float Price { get; set; }
}
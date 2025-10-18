using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Fast_Food_Delievery.ViewModel;

public class ItemViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Display(Name = "Sub Category")]
    public int? SubCategoryId { get; set; }
    // دي الصورة اللي جاية من المستخدم لما يرفع جديدة
    public IFormFile? ImageFile { get; set; }

    // ودي المسار القديم اللي متخزن في الداتابيز
    public string? ExistingImageUrl { get; set; }
}

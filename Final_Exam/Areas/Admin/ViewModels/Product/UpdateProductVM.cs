using System.ComponentModel.DataAnnotations;

namespace Final_Exam.Areas.Admin.ViewModels;

public class UpdateProductVM
{
    [Required]
    [MaxLength(75)]
    public string Name { get; set; }

    public string? ImageUrl { get; set; }

    public IFormFile? Photo { get; set; }

    [Required]
    [MaxLength(75)]
    public string Catagory { get; set; }
}

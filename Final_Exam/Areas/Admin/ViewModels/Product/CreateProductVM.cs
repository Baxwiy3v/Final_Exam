using System.ComponentModel.DataAnnotations;

namespace Final_Exam.Areas.Admin.ViewModels;

public class CreateProductVM
{
    [Required]
    [MaxLength(75)]
    public string Name { get; set; }

    [Required]

    public IFormFile Photo { get; set; }

    [Required]
    [MaxLength(75)]
    public string Catagory { get; set; }
}

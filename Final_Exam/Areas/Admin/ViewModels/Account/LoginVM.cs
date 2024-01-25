using System.ComponentModel.DataAnnotations;

namespace Final_Exam.Areas.Admin.ViewModels;

public class LoginVM
{
    [Required]

    public string UserOrEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]

    public string Password { get; set; }

    public bool RememberMe { get; set; }


}

using System.ComponentModel.DataAnnotations;

namespace Final_Exam.Areas.Admin.ViewModels
{
    public class UpdateSettingVM
    {
        [Required]
        public string Key { get; set; }
        [Required]

        public string Value { get; set; }
    }
}

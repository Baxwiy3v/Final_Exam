using Microsoft.AspNetCore.Identity;

namespace Final_Exam.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}

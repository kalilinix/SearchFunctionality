using System.ComponentModel.DataAnnotations;

namespace SearchFunction.Models

{
    public class EmployeeDetails
    {
        [Required(ErrorMessage = "Please enter your ID")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Please enter your Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your Gender")]
        public string? Gender { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid Email Address")]
        [Required(ErrorMessage = "Please enter your Email")]

        public string? Email { get; set; }
    }


    public class EmployeeSearchEnt
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
    }
}

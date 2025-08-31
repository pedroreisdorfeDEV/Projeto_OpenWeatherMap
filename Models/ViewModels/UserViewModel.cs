using System.ComponentModel.DataAnnotations;

namespace projetoGloboClima.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string User { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }

}

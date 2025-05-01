using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Core
{
    public class UserRole
    {
        [Key]
        public int IdUserRole { get; set; }

        [Required(ErrorMessage = "The Role Name field is required.")]
        public string Role { get; set; }

        public ICollection<User> Users { get; } = new List<User>();
    }
}

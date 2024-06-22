using System.ComponentModel.DataAnnotations;

namespace ToDoList.Data.UserData
{
    public class User
    {
        [Key]
        public required int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Password { get; set; }

        // Navigation property to link with notes
        public ICollection<Note> Notes { get; set; }
    }
}

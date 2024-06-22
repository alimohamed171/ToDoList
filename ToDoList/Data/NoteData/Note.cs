using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ToDoList.Data.UserData;

namespace ToDoList.Data
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        // Navigation property to link with user
        public User User { get; set; }
    }
}

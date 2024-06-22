using System.ComponentModel.DataAnnotations;

namespace ToDoList.Data.UserData
{
    public class UserSignupDTO
    {

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}

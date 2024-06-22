using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using ToDoList.Data;
using ToDoList.Data.NoteData;
using ToDoList.Data.UserData;
using static Azure.Core.HttpHeader;

namespace ToDoList.Services
{
    public interface IUserServices
    {
        User login(UserLoginDTO user);

        User createUser(UserSignupDTO user);

        User updateUser(UserUpdateDTO user);

        public UserDTO get(int id);

    }

    public class UserServices : IUserServices
    {
        private readonly Regex EmailRegex = new Regex(
          @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase
           );

        private readonly ApplicationDbContext _db;

        public UserServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public User login(UserLoginDTO user)
        {
            var dbUser = _db.Users.
                FirstOrDefault(u => u.Email.Equals(user.Email));

            if (dbUser != null && BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {

                return dbUser;
            }


            return null;
        }

        public User createUser(UserSignupDTO user)
        {
            if (!EmailRegex.IsMatch(user.Email))
            {
                throw new InvalidOperationException("Invalid email format");
            }

            if (_db.Users.Any(u => u.Email.Equals(user.Email)))
            {
                throw new InvalidOperationException("User already exists");
            }
              user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            User userDb = new User{
                Id = 0,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password, // Hash the password
                Notes = new List<Note>()
            };
            //user.Notes = new List<Note>();
            _db.Users.Add(userDb);
            _db.SaveChanges();
            return userDb;
        }

     
       
        public User updateUser(UserUpdateDTO user)
        {
            throw new NotImplementedException();
        }

        public UserDTO get(int id)
        {
            var user = _db.Users.Include(u => u.Notes)
                       .FirstOrDefault(u => u.Id == id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
             
                Notes = user.Notes.Select(n => new NoteResponesDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    IsPublic = n.IsPublic
                }).ToList()
            };
        }
    }
}

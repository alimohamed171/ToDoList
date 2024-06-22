using ToDoList.Data.NoteData;

namespace ToDoList.Data.UserData
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<NoteResponesDTO> Notes { get; set; }
    }
}

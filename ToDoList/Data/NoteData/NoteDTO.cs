namespace ToDoList.Data.NoteData
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public int UserId { get; set; }
    }
}


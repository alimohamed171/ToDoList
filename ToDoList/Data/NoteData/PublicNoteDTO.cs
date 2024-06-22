namespace ToDoList.Data.NoteData
{
    public class PublicNoteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public int UserId { get; set; }
        public string CreatorName { get; set; }
    }
}

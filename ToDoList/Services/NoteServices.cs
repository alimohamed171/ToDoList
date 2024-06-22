using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Data.NoteData;

namespace ToDoList.Services
{

    public interface INoteServices
    {
        Task<IEnumerable<PublicNoteDTO>> GetPrivateNotesByUserIdAsync(int userId);

        Task<IEnumerable<PublicNoteDTO>> GetAllNotesAsync();

        Task<NoteDTO> CreateNoteAsync(NoteDTO noteDto);

        Task<bool> UpdateNoteAsync(int noteId, NoteDTO noteDto);

        Task<bool> DeleteNoteAsync(int noteId);
    }
    public class NoteServices : INoteServices
    {
        private readonly ApplicationDbContext _dbContext;

        public NoteServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }


        public async Task<NoteDTO> CreateNoteAsync(NoteDTO noteDto)
        {
            var note = new Note { 
            Title = noteDto.Title,
            Content = noteDto.Content,  
            IsPublic = noteDto.IsPublic,
            UserId = noteDto.UserId
            
            };
            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();
            noteDto.Id = note.Id; // Assign the generated Id back to DTO
            return noteDto;
        }

        public Task<bool> DeleteNoteAsync(int noteId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PublicNoteDTO>> GetAllNotesAsync()
        {
           var publicNotes = await _dbContext.Notes
                .Where(n => n.IsPublic)
                .Include(n => n.User)
                .Select(n => new PublicNoteDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    IsPublic = n.IsPublic,
                    UserId = n.UserId,
                    CreatorName = n.User.Name

                }).ToListAsync();
            return publicNotes;
        }

        public async Task<IEnumerable<PublicNoteDTO>> GetPrivateNotesByUserIdAsync(int userId)
        {
            var publicNotes = await _dbContext.Notes
                .Where(n => !n.IsPublic && n.UserId == userId )
                .Include(n => n.User)
                .Select(n => new PublicNoteDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    IsPublic = n.IsPublic,
                    UserId = n.UserId,
                    CreatorName = n.User.Name

                }).ToListAsync();

            return publicNotes;
        }

        public Task<bool> UpdateNoteAsync(int noteId, NoteDTO noteDto)
        {
            throw new NotImplementedException();
        }
    }
}

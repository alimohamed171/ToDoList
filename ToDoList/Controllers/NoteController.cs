using Microsoft.AspNetCore.Mvc;
using ToDoList.Data.NoteData;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {

        private readonly INoteServices _noteService;

        public NoteController(INoteServices noteService)
        {
            _noteService = noteService;
        }


        // POST api/notes
        [HttpPost]
        [Route("CreateNote")]
        public async Task<ActionResult<NoteDTO>> CreateNote([FromBody] NoteDTO noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdNoteDto = await _noteService.CreateNoteAsync(noteDto);
            return createdNoteDto;
        }

        [HttpGet("publicNote")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetAllPublicNotes()
        {
            var publicNotes = await _noteService.GetAllNotesAsync();
            return Ok(publicNotes);
        }
        [HttpGet("privateNotes/{userId}")]
        public async Task<ActionResult<IEnumerable<PublicNoteDTO>>> GetPrivateNotesByUserId(int userId)
        {
            var privateNotes = await _noteService.GetPrivateNotesByUserIdAsync(userId);
            if (privateNotes == null || !privateNotes.Any())
            {
                return NotFound();
            }

            return Ok(privateNotes);
        }


    }
}

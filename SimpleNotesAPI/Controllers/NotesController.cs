using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleNotesAPI.Data;
using SimpleNotesAPI.Model;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly AppDbContext _db;

    public NotesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<NoteDTO>> AddNote([FromBody] NoteDTO noteDto)
    {
        var note = new Note
        {
            Title = noteDto.Title,
            Content = noteDto.Content,
            CreatedDate = DateTime.UtcNow
        };

        _db.Notes.Add(note);
        await _db.SaveChangesAsync();

        var resultDto = new NoteDTO
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content
        };

        return CreatedAtAction(nameof(GetNotes), new { id = note.Id }, resultDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotes()
    {
        var notes = await _db.Notes
            .OrderByDescending(n => n.CreatedDate)
            .Select(n => new NoteDTO { Id = n.Id, Title = n.Title, Content = n.Content })
            .ToListAsync();
        return Ok(notes);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditNote(int id, [FromBody] NoteDTO noteDto)
    {
        var note = await _db.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        note.Title = noteDto.Title;
        note.Content = noteDto.Content;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<NoteDTO>>> SearchNotes([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return BadRequest("Title query parameter is required.");
        }
        var notes = await _db.Notes
            .Where(n => n.Title.ToLower().Contains(title.ToLower()))
            .Select(n => new NoteDTO { Id = n.Id, Title = n.Title, Content = n.Content })
            .ToListAsync();
        return Ok(notes);
    }
}

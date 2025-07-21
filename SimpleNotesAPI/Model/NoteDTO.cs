
namespace SimpleNotesAPI.Model
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}

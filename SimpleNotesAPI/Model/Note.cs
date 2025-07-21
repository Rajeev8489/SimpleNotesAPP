namespace SimpleNotesAPI.Model
{
    public class Note
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

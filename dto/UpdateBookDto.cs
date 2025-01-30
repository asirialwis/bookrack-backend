namespace BookManagerAPI.Dtos
{
    public class UpdateBookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}

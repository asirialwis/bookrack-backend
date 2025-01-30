using System.ComponentModel.DataAnnotations;
public class Book
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Title { get; set; }

    [Required]
    public required string Author { get; set; }

    [MaxLength(13)]
    public string ISBN { get; set; }

    [Required]
    public DateTime PublicationDate { get; set; }
}

namespace CareerApplication.Core.Entities;

public class RatingEntity : BaseEntity
{
    public static string Node { get => "Ratings"; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comments { get; set; } = string.Empty;
}

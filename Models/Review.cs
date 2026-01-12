using System.ComponentModel.DataAnnotations.Schema;

namespace mvc.Models;

public class Review
{
    public int id { get; set; }

    [ForeignKey("Users")]

    public string? userID { get; set; }

    public Users? Users { get; set; }
    public int nota { get; set; }
    public string? review { get; set; }
}

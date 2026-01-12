using System.ComponentModel.DataAnnotations.Schema;

namespace mvc.Models;

public class Contact
{
    public int id { get; set; }

    public string message { get; set; }

    public DateTime timeSent { get; set; }

    [ForeignKey("Users")]

    public string userID { get; set; }

    public Users? Users { get; set; }
}

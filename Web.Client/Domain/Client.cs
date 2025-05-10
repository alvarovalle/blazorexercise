using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Client
{
    public Guid Number { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
    [AllowedValues(false, ErrorMessage = "Email already in use")]
    public bool EmailExists { get; set; }
    public void RegisterNewClient()
    {
        if (Number == Guid.Empty)
            Number = Guid.NewGuid();
    }
}

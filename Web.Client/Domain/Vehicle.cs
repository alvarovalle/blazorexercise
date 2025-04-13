using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Vehicle
{
    public Guid Number { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int NumberSeats { get; set; }
    [Required]
    public int NumberDoors { get; set; }
    public void RegisterNewVehicle()
    {
        if (Number == Guid.Empty)
            Number = Guid.NewGuid();
    }
}

namespace libraryWebApp.DTOs;

public class PublishingHouseDto
{
    public int PublishingHouseId { get; set; }
    public string Name { get; set; } = null!;
    public string HousePhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
}
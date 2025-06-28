namespace application.BL.Models.Lists;

public class CityListModel
{
    public required int Id { get; set; }
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
    public required string CityName { get; set; }
    public required string StateName { get; set; }
}
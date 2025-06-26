namespace application.BL.Models.Lists;

public class CityListModel
{
    public int Id { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CityName { get; set; } = null!;
    public string StateName { get; set; } = null!;
}
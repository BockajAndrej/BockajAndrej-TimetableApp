namespace application.BL.Models.Lists;

public class EmployeeListModel
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public DateOnly BirthDay { get; set; }
    public string BirthNumber { get; set; } = null!;
}
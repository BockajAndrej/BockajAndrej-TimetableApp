namespace application.BL.Models.Lists;

public class EmployeeListModel
{
    public required string Id { get; set; } = null!;

    public required string FirstName { get; set; } = null!;
    public required string LastName { get; set; } = null!;

    public required DateOnly BirthDay { get; set; }
    public required string BirthNumber { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";
}
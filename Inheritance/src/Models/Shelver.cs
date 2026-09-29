namespace LibrarySystem.Models;

public sealed class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary,
        string section)
        : base(personId, fullName, phone, hireDate, monthlySalary, 0)
    {
        if (string.IsNullOrWhiteSpace(section)) throw new ArgumentException("Section is required.");
        Section = section;
    }

    public void Reassign(string newSection)
    {
        if (string.IsNullOrWhiteSpace(newSection)) throw new ArgumentException("Section is required.");
        Section = newSection;
    }
}

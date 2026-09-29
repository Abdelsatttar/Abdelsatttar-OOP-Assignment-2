namespace LibrarySystem.Models;

public sealed class Librarian : Staff
{
    public Librarian(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary)
        : base(personId, fullName, phone, hireDate, monthlySalary, 0)
    {
    }

    public void ProcessReturn(Loan loan, DateTime returnDate)
    {
        if (loan == null) throw new ArgumentNullException(nameof(loan));
        loan.Return(returnDate);
    }

    public void MarkAsLost(Loan loan)
    {
        if (loan == null) throw new ArgumentNullException(nameof(loan));
        loan.MarkAsLost();
    }
}

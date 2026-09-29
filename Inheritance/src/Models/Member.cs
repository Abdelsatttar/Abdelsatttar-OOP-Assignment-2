namespace LibrarySystem.Models;

public class Member : Person
{
    private readonly List<Loan> _loans = new();
    private readonly int _maximumActiveLoans;
    private readonly decimal _lateFeeDiscountPercentage;

    public IReadOnlyList<Loan> Loans
    {
        get { return _loans.AsReadOnly(); }
    }
    public int MaximumActiveLoans
    {
        get { return _maximumActiveLoans; }
    }
    public decimal LateFeeDiscountPercentage
    {
        get { return _lateFeeDiscountPercentage; }
    }

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maximumActiveLoans,
        decimal lateFeeDiscountPercentage)
        : base(personId, fullName, phone)
    {
        if (maximumActiveLoans <= 0) throw new ArgumentException("Maximum active loans must be positive.");
        if (lateFeeDiscountPercentage < 0 || lateFeeDiscountPercentage > 100)
            throw new ArgumentException("Late-fee discount must be between 0 and 100 percent.");

        _maximumActiveLoans = maximumActiveLoans;
        _lateFeeDiscountPercentage = lateFeeDiscountPercentage;
    }

    public Loan Borrow(LibraryItem item, DateTime borrowDate)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (GetActiveLoansCount() >= _maximumActiveLoans)
            throw new InvalidOperationException("The member has reached the maximum number of active loans.");
        if (item.IsWithdrawn)
            throw new InvalidOperationException("A withdrawn item cannot be borrowed.");
        if (item.IsOnLoan)
            throw new InvalidOperationException("The item is already on loan.");

        var loan = new Loan(this, item, borrowDate);
        _loans.Add(loan);
        item.MarkAsOnLoan();
        return loan;
    }

    public int GetActiveLoansCount()
    {
        var count = 0;
        foreach (var loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                count++;
        }

        return count;
    }
}

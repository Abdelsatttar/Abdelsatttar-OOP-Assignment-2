namespace LibrarySystem.Models;

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }
    public decimal BaseLateFee { get; private set; }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(
        string catalogNumber,
        string title,
        int loanPeriodDays,
        decimal baseLateFee,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber)) throw new ArgumentException("Catalog number is required.");
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
        if (loanPeriodDays <= 0) throw new ArgumentException("Loan period must be positive.");
        if (baseLateFee <= 0) throw new ArgumentException("Base late fee must be greater than zero.");
        if (lateFeeMultiplier <= 0) throw new ArgumentException("Late-fee multiplier must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        LoanPeriodDays = loanPeriodDays;
        BaseLateFee = baseLateFee;
        _lateFeeMultiplier = lateFeeMultiplier;
    }

    private readonly decimal _lateFeeMultiplier;

    public decimal GetDailyLateFee()
    {
        return BaseLateFee * _lateFeeMultiplier;
    }

    public void SetBaseLateFee(decimal newFee)
    {
        if (newFee <= 0) throw new ArgumentException("New late fee must be greater than zero.");
        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        if (IsOnLoan) throw new InvalidOperationException("An item on loan cannot be withdrawn.");
        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    internal void MarkAsOnLoan()
    {
        if (IsOnLoan) throw new InvalidOperationException("The item is already on loan.");
        if (IsWithdrawn) throw new InvalidOperationException("A withdrawn item cannot be borrowed.");
        IsOnLoan = true;
    }

    internal void MarkAsAvailable()
    {
        IsOnLoan = false;
    }
}

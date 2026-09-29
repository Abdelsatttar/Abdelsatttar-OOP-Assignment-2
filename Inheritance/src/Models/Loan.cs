namespace LibrarySystem.Models;

public sealed class Loan
{
    private static int _nextLoanNumber = 1;

    public string LoanId { get; }
    public DateTime BorrowDate { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public LoanStatus Status { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public DateTime DueDate
    {
        get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
    }

    public decimal LateFee
    {
        get
        {
            if (ReturnDate == null || ReturnDate.Value <= DueDate)
                return 0;

            var lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;
            var grossFee = lateDays * Item.GetDailyLateFee();
            var discount = grossFee * Member.LateFeeDiscountPercentage / 100m;
            return Math.Max(0, grossFee - discount);
        }
    }

    public Loan(Member member, LibraryItem item, DateTime borrowDate)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (item == null) throw new ArgumentNullException(nameof(item));

        Member = member;
        Item = item;
        BorrowDate = borrowDate;
        LoanId = $"L-{_nextLoanNumber:D4}";
        _nextLoanNumber++;
        Status = LoanStatus.Borrowed;
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be returned.");
        if (returnDate < BorrowDate)
            throw new InvalidOperationException("Return date cannot be earlier than the borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.MarkAsAvailable();
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.MarkAsAvailable();
    }
}

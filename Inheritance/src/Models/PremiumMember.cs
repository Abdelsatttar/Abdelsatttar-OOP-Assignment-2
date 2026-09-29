namespace LibrarySystem.Models;

public sealed class PremiumMember : Member
{
    public PremiumMember(string personId, string fullName, string phone, decimal discountPercentage)
        : base(personId, fullName, phone, 10, discountPercentage)
    {
    }

    public int ReadingPoints
    {
        get
        {
            var points = 0;
            foreach (var loan in Loans)
            {
                if (loan.Status == LoanStatus.Returned)
                    points += 5;
            }

            return points;
        }
    }
}

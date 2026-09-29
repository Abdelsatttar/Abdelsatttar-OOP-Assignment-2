namespace SrpLab;

public sealed class LedgerJournalFormatter
{
    public string Format(Subscription subscription, string invoiceNumber, decimal amount)
    {
        return $"{subscription.CustomerId},{invoiceNumber},{amount:0.00},AR-SUB";
    }
}

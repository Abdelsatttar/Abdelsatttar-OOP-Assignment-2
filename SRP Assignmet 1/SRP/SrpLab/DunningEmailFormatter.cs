namespace SrpLab;

public sealed class DunningEmailFormatter
{
    public string Format(Subscription subscription, string customerName, DateOnly asOf, decimal amount, string invoiceNumber)
    {
        var severity = subscription.FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoiceNumber}\nHi {customerName},\nBalance {amount:C} as of {asOf:o} ({subscription.FailedPayments} failures).\n";
    }
}

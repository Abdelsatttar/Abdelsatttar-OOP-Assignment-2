namespace SrpLab;

public sealed class InternalEscalationFormatter
{
    public string Format(SupportTicket ticket, string priority, DateTimeOffset deadline)
    {
        return $"ESCALATE {ticket.Id} priority={priority} breachAt={deadline:u} keywords-scanned=yes";
    }
}

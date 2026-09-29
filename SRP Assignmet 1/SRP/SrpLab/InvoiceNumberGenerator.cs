namespace SrpLab;

public sealed class InvoiceNumberGenerator
{
    private int _sequence = 1000;

    public string Next(DateOnly periodStart)
    {
        var number = ++_sequence;
        return $"INV-{periodStart:yyyyMM}-{number:D5}";
    }
}

namespace SrpLab;

public sealed class PagerAlertService
{
    private readonly List<string> _log = new();

    public void CheckAndRecord(int bed, int acuityScore)
    {
        if (acuityScore >= 8)
            _log.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
    }

    public IReadOnlyList<string> DrainLog()
    {
        var copy = _log.ToList();
        _log.Clear();
        return copy;
    }
}

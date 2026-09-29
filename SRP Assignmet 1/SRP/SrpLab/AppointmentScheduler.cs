namespace SrpLab;

public sealed class AppointmentScheduler
{
    private readonly BusinessHoursPolicy _hours;
    private readonly HashSet<DateTimeOffset> _booked = new();

    public AppointmentScheduler(BusinessHoursPolicy hours)
    {
        _hours = hours;
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);
        while (cursor < end)
        {
            if (_hours.IsWithinHours(cursor) && !_booked.Contains(cursor)) return cursor;
            cursor = cursor.AddMinutes(_hours.SlotMinutes);
        }
        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!_hours.IsWithinHours(slot) || _booked.Contains(slot)) return false;
        _booked.Add(slot);
        return true;
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % _hours.SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}

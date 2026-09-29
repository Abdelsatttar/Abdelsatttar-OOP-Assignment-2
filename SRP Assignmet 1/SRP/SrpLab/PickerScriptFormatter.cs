namespace SrpLab;

public sealed class PickerScriptFormatter
{
    public string Format(WarehousePickList pickList, IReadOnlyList<(string Sku, int Allocated)> allocations,
        IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder)
    {
        var steps = walkingOrder.Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");
        var shortfalls = allocations.Where(a =>
        {
            var need = pickList.Lines.First(l => l.Sku == a.Sku).QtyNeeded;
            return a.Allocated < need;
        });

        var warn = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";

        return string.Join('\n', steps) + "\n" + warn;
    }
}

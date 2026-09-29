namespace SrpLab;

public sealed class WalkingOrderCalculator
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> Calculate(WarehousePickList pickList)
    {
        return pickList.Lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => (l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0)
            .ToList();
    }
}

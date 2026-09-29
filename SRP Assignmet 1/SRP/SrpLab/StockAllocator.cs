namespace SrpLab;

public sealed class StockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(WarehousePickList pickList)
    {
        return pickList.Lines
            .Select(line => (line.Sku, Allocated: Math.Min(line.QtyNeeded, line.QtyOnHand)))
            .ToList();
    }
}

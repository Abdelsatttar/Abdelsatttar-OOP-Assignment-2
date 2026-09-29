namespace SrpLab;

public sealed class WarehousePickList
{
    private readonly List<PickRequirement> _lines = new();
    public IReadOnlyList<PickRequirement> Lines => _lines;

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add(new PickRequirement(sku, aisle, bin, qtyNeeded, qtyOnHand));
    }
}

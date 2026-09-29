namespace SrpLab;

public sealed class KitchenOrder
{
    private readonly List<KitchenItem> _items = new();

    public IReadOnlyList<KitchenItem> Items => _items;

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add(new KitchenItem(
            item,
            ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(),
            prepMinutes));
    }
}

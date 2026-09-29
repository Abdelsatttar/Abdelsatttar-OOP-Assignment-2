namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<BasketLine> _lines = new();
    public string? CouponText { get; private set; }
    public bool GiftWrapEnabled { get; private set; }

    public IReadOnlyList<BasketLine> Lines => _lines;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add(new BasketLine(sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => CouponText = couponText;
    public void EnableGiftWrap() => GiftWrapEnabled = true;

    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);
}

namespace SrpLab;

public sealed class CheckoutTotalCalculator
{
    public decimal Calculate(CheckoutBasket basket, CouponDiscountCalculator discounts)
    {
        var total = basket.SubTotal() - discounts.Calculate(basket.CouponText, basket.SubTotal());
        if (basket.GiftWrapEnabled) total += 4.99m;
        return Math.Max(0m, total);
    }
}

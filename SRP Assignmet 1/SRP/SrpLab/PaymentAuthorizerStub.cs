namespace SrpLab;

public sealed class PaymentAuthorizerStub
{
    public string Authorize(CheckoutBasket basket, string cardLast4, decimal total)
    {
        var payload = $"{total:0.00}|{cardLast4}|{basket.Lines.Count}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}

namespace SrpLab;

public sealed class KitchenReadyTimeCalculator
{
    public int Calculate(KitchenOrder order, int openStations, int allergenCount)
    {
        if (openStations <= 0) openStations = 1;
        var sequential = order.Items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (allergenCount > 0) parallel += 3;

        var longest = order.Items.Count == 0 ? 0 : order.Items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }
}

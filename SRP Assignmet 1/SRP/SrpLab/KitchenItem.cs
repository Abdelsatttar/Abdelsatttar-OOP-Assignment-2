namespace SrpLab;

public sealed record KitchenItem(string Item, IReadOnlyList<string> Ingredients, int PrepMinutes);

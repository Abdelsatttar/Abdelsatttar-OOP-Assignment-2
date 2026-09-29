namespace SrpLab;

public sealed record PickRequirement(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand);

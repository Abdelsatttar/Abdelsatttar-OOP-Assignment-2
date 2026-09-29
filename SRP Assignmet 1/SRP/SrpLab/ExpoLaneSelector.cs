namespace SrpLab;

public sealed class ExpoLaneSelector
{
    public string Select(int allergenCount, int etaMinutes)
    {
        return allergenCount > 0 ? "LANE-ALLERGY" : etaMinutes > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}

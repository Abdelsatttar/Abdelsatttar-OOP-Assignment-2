namespace SrpLab;

public sealed class WardCensus
{
    private readonly Dictionary<int, string> _patients = new();
    private readonly Dictionary<int, int> _acuityScores = new();

    public void AssignBed(int bed, string patientId, int acuityScore)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _patients[bed] = patientId.Trim().ToUpperInvariant();
        _acuityScores[bed] = acuityScore;
    }

    public bool TryGetPatient(int bed, out string patientId)
    {
        return _patients.TryGetValue(bed, out patientId!);
    }

    public int GetAcuity(int bed) => _acuityScores[bed];

    public IReadOnlyDictionary<int, string> Patients => _patients;
    public IReadOnlyDictionary<int, int> AcuityScores => _acuityScores;
}

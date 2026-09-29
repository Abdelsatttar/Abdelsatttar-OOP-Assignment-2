namespace SrpLab;

public sealed class LoanUnderwriterCsvFormatter
{
    public string Format(string applicationId, LoanApplication application, decimal riskScore, bool eligible)
    {
        return $"{applicationId},{application.CreditScore},{application.EmploymentMonths},{(application.HasCollateral ? 1 : 0)},{riskScore:0.00},{(eligible ? "Y" : "N")}";
    }
}

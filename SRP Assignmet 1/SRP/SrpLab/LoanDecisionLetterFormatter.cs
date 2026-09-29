namespace SrpLab;

public sealed class LoanDecisionLetterFormatter
{
    public string Format(LoanApplication application, string applicantName, decimal riskScore, bool eligible, IReadOnlyList<string> documents)
    {
        if (eligible)
        {
            return $"Dear {applicantName},\nYour request for {application.RequestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                   $"Please upload: {string.Join("; ", documents)}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {application.RequestedAmount:C} at this time.\n" +
               $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
    }
}

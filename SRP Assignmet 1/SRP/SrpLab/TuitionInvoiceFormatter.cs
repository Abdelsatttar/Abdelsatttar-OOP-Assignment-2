namespace SrpLab;

public sealed class TuitionInvoiceFormatter
{
    public string Format(CourseEnrollment enrollment, string studentEmail)
    {
        if (!enrollment.IsSeated(studentEmail)) return $"{enrollment.CourseCode},WAITLIST,0.00";

        var vat = Math.Round(enrollment.Tuition * 0.14m, 2);
        return $"{enrollment.CourseCode},TUITION,{enrollment.Tuition:0.00},VAT,{vat:0.00},TOTAL,{(enrollment.Tuition + vat):0.00}";
    }
}

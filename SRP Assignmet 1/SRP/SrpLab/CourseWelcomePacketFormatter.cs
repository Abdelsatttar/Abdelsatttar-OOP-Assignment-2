namespace SrpLab;

public sealed class CourseWelcomePacketFormatter
{
    public string Format(CourseEnrollment enrollment, string studentEmail, string studentName)
    {
        var status = enrollment.IsSeated(studentEmail)
            ? "confirmed seat"
            : $"waitlist #{enrollment.WaitlistPosition(studentEmail)}";

        return $"# Welcome to {enrollment.CourseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{enrollment.CourseCode.ToLowerInvariant()}\n";
    }
}

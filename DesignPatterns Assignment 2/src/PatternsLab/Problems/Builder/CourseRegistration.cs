namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    private CourseRegistration(Builder builder)
    {
        StudentEmail = builder.StudentEmail!;
        CourseCode = builder.CourseCode!;
        AccessMode = builder.AccessMode!;
        GroupCode = builder.GroupCode;
        DiscountCode = builder.DiscountCode;
        SendWhatsApp = builder.SendWhatsApp;
        SendEmailWelcome = builder.SendEmailWelcome;
        MentorNote = builder.MentorNote;
        PreferredStart = builder.PreferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";

    public sealed class Builder
    {
        public string? StudentEmail { get; private set; }
        public string? CourseCode { get; private set; }
        public string? AccessMode { get; private set; }
        public string? GroupCode { get; private set; }
        public string? DiscountCode { get; private set; }
        public bool SendWhatsApp { get; private set; }
        public bool SendEmailWelcome { get; private set; }
        public string? MentorNote { get; private set; }
        public DateOnly? PreferredStart { get; private set; }

        public Builder ForStudent(string email)
        {
            StudentEmail = email;
            return this;
        }

        public Builder ForCourse(string courseCode)
        {
            CourseCode = courseCode;
            return this;
        }

        public Builder LiveGroup()
        {
            AccessMode = "LiveGroup";
            return this;
        }

        public Builder VideosOnly()
        {
            AccessMode = "VideosOnly";
            return this;
        }

        public Builder WithGroupCode(string? groupCode)
        {
            GroupCode = groupCode;
            return this;
        }

        public Builder WithDiscountCode(string discountCode)
        {
            DiscountCode = discountCode;
            return this;
        }

        public Builder SendWhatsAppWelcome()
        {
            SendWhatsApp = true;
            return this;
        }

        public Builder SendEmailWelcomeMessage()
        {
            SendEmailWelcome = true;
            return this;
        }

        public Builder WithMentorNote(string mentorNote)
        {
            MentorNote = mentorNote;
            return this;
        }

        public Builder WithPreferredStart(DateOnly preferredStart)
        {
            PreferredStart = preferredStart;
            return this;
        }

        public CourseRegistration Build()
        {
            if (string.IsNullOrWhiteSpace(StudentEmail))
                throw new ArgumentException("Student email is required.");

            if (string.IsNullOrWhiteSpace(CourseCode))
                throw new ArgumentException("Course code is required.");

            if (AccessMode is not "LiveGroup" and not "VideosOnly")
                throw new InvalidOperationException("Access mode must be LiveGroup or VideosOnly.");

            if (AccessMode == "LiveGroup" && string.IsNullOrWhiteSpace(GroupCode))
                throw new InvalidOperationException("LiveGroup requires GroupCode.");

            if (AccessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(GroupCode))
                throw new InvalidOperationException("VideosOnly cannot have GroupCode.");

            return new CourseRegistration(this);
        }
    }
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudent()
    {
        return new CourseRegistration.Builder()
            .ForStudent("sara@mail.com")
            .ForCourse("SEF-101")
            .LiveGroup()
            .WithGroupCode("G1")
            .WithDiscountCode("EARLY10")
            .SendWhatsAppWelcome()
            .SendEmailWelcomeMessage()
            .WithMentorNote("Needs evening slot")
            .WithPreferredStart(new DateOnly(2026, 10, 1))
            .Build();
    }

    public static CourseRegistration CreateVideosOnlyStudent()
    {
        return new CourseRegistration.Builder()
            .ForStudent("ali@mail.com")
            .ForCourse("SEF-101")
            .VideosOnly()
            .SendEmailWelcomeMessage()
            .Build();
    }
}

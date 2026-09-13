namespace FintechJobPortal.Core.Enums;

public enum ExperienceLevel
{
    All,
    EntryLevel,
    MidLevel,
    Senior,
    LeadStaff,
    DirectorExecutive
}

public static class ExperienceLevelExtensions
{
    public static string ToDisplayString(this ExperienceLevel level) => level switch
    {
        ExperienceLevel.EntryLevel => "Entry / Graduate",
        ExperienceLevel.MidLevel => "Mid-Level (2-4 yrs)",
        ExperienceLevel.Senior => "Senior (5-8 yrs)",
        ExperienceLevel.LeadStaff => "Lead / Staff (8+ yrs)",
        ExperienceLevel.DirectorExecutive => "Director / VP / Exec",
        _ => "All Experience Levels"
    };
}

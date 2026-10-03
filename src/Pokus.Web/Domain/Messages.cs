namespace Pokus.Web.Domain;

public enum Tone
{
    Neutral,
    Casual,
    Witty,
    Filipino,
    Chaotic,
}

public enum MessageCategory
{
    DashboardGreeting,
    NudgeStart,
    SessionStart,
    SessionMid,
    BreakStart,
    SessionComplete,
    SessionCancel,
    CardsDue,
    ReviewComplete,
    EmptyState,
}

/// <summary>A message line. "{0}" is replaced with a count where the category needs one (e.g. cards due).</summary>
public sealed record MessageTemplate(MessageCategory Category, Tone Tone, string Text);

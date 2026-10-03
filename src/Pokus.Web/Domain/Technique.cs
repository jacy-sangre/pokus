namespace Pokus.Web.Domain;

public enum EvidenceLevel
{
    ResearchSupported,
    CommonlyUsed,
    PracticalHeuristic,
    MixedOrLimited,
}

public enum Difficulty
{
    Easy,
    Moderate,
    Advanced,
}

public sealed record Technique(
    string Slug,
    string Name,
    IReadOnlyList<string> Categories,
    EvidenceLevel Evidence,
    Difficulty Difficulty,
    string SessionLength,
    string ShortDescription)
{
    /// <summary>Name without a trailing "Technique", e.g. "Pomodoro" for compact lists.</summary>
    public string ShortName => Name.EndsWith(" Technique") ? Name[..^" Technique".Length] : Name;
}

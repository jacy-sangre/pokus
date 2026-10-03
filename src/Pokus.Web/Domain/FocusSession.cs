namespace Pokus.Web.Domain;

public enum SessionStatus
{
    Active,
    Paused,
    Completed,
    Cancelled,
}

public sealed record FocusPreset(string Key, string Name, int FocusMinutes, int BreakMinutes);

/// <summary>
/// A focus session. Remaining time is always derived from timestamps, never stored as a counter.
/// </summary>
public sealed class FocusSession
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required FocusPreset Preset { get; init; }
    public string? TechniqueSlug { get; init; }
    public string? Task { get; set; }
    public string? Notes { get; set; }
    public required DateTimeOffset StartedAt { get; init; }
    public DateTimeOffset? PausedAt { get; set; }
    public TimeSpan PausedTotal { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Active;

    public TimeSpan PlannedFocus => TimeSpan.FromMinutes(Preset.FocusMinutes);

    public TimeSpan Elapsed(DateTimeOffset now)
    {
        var end = EndedAt ?? PausedAt ?? now;
        var elapsed = end - StartedAt - PausedTotal;
        return elapsed < TimeSpan.Zero ? TimeSpan.Zero : elapsed;
    }

    public TimeSpan Remaining(DateTimeOffset now)
    {
        var remaining = PlannedFocus - Elapsed(now);
        return remaining < TimeSpan.Zero ? TimeSpan.Zero : remaining;
    }

    /// <summary>Share of the focus phase that has passed, from 0 to 1.</summary>
    public double Progress(DateTimeOffset now) => Math.Clamp(Elapsed(now) / PlannedFocus, 0, 1);
}

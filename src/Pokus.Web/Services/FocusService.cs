using Pokus.Web.Data;
using Pokus.Web.Domain;

namespace Pokus.Web.Services;

public interface IFocusService
{
    FocusSession? Current { get; }
    IReadOnlyList<FocusSession> History { get; }

    /// <summary>Focus minutes for each day Monday..Sunday of the current week.</summary>
    int[] MinutesThisWeek();
}

public sealed class InMemoryFocusService : IFocusService
{
    private readonly TimeProvider clock;
    private readonly List<FocusSession> history;

    public InMemoryFocusService(TimeProvider clock)
    {
        this.clock = clock;
        (Current, history) = DemoSeed.Sessions(clock.GetLocalNow());
    }

    public FocusSession? Current { get; private set; }

    public IReadOnlyList<FocusSession> History => history;

    public int[] MinutesThisWeek()
    {
        var now = clock.GetLocalNow();
        var monday = Week.Monday(now);
        var minutes = new int[7];
        foreach (var s in history.Where(s => s.Status == SessionStatus.Completed && s.StartedAt >= monday))
        {
            var day = (int)(s.StartedAt.Date - monday.Date).TotalDays;
            if (day is >= 0 and < 7) minutes[day] += (int)Math.Round(s.Elapsed(now).TotalMinutes);
        }
        return minutes;
    }
}

public static class Week
{
    public static DateTimeOffset Monday(DateTimeOffset now)
    {
        var offset = ((int)now.DayOfWeek + 6) % 7; // Monday = 0
        return new DateTimeOffset(now.Date.AddDays(-offset), now.Offset);
    }

    /// <summary>Index of today in a Monday-first week.</summary>
    public static int TodayIndex(DateTimeOffset now) => ((int)now.DayOfWeek + 6) % 7;
}

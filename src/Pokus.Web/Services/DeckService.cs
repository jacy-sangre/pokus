using Pokus.Web.Data;
using Pokus.Web.Domain;

namespace Pokus.Web.Services;

public interface IDeckService
{
    IReadOnlyList<Subject> Subjects { get; }
    IReadOnlyList<Deck> Decks { get; }
    IReadOnlyList<CardReview> Reviews { get; }
    int TotalDue();
    int ReviewedThisWeek();
}

public sealed class InMemoryDeckService : IDeckService
{
    private readonly TimeProvider clock;
    private readonly List<Subject> subjects;
    private readonly List<Deck> decks;
    private readonly List<CardReview> reviews;

    public InMemoryDeckService(TimeProvider clock)
    {
        this.clock = clock;
        var now = clock.GetLocalNow();
        (subjects, decks) = DemoSeed.Flashcards(now);
        reviews = DemoSeed.Reviews(decks, now);
    }

    public IReadOnlyList<Subject> Subjects => subjects;
    public IReadOnlyList<Deck> Decks => decks;
    public IReadOnlyList<CardReview> Reviews => reviews;

    public int TotalDue()
    {
        var now = clock.GetLocalNow();
        return decks.Sum(d => d.DueCount(now));
    }

    public int ReviewedThisWeek()
    {
        var monday = Week.Monday(clock.GetLocalNow());
        return reviews.Count(r => r.ReviewedAt >= monday);
    }
}

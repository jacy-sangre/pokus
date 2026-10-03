namespace Pokus.Web.Domain;

public sealed class Subject
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
}

public sealed class Deck
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid? SubjectId { get; set; }
    public required string Name { get; set; }
    public bool UseSpacedRepetition { get; set; } = true;
    public List<Flashcard> Cards { get; } = [];

    public int DueCount(DateTimeOffset now) => UseSpacedRepetition ? Cards.Count(c => c.IsDue(now)) : 0;
}

public sealed class Flashcard
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Front { get; set; }
    public required string Back { get; set; }
    public List<string> Tags { get; } = [];

    /// <summary>Leitner box, 0 (new or just forgotten) to 5.</summary>
    public int Box { get; set; }
    public DateTimeOffset DueAt { get; set; }
    public DateTimeOffset? LastReviewedAt { get; set; }

    public bool IsDue(DateTimeOffset now) => DueAt <= now;
}

public sealed record CardReview(Guid CardId, DateTimeOffset ReviewedAt, bool Knew);

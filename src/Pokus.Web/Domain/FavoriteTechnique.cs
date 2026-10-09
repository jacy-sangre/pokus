namespace Pokus.Web.Domain;

/// <summary>A technique the user bookmarked. Techniques are seeded content, so the link is by slug.</summary>
public sealed class FavoriteTechnique
{
    public Guid UserId { get; init; }
    public required string TechniqueSlug { get; init; }
    public DateTimeOffset AddedAt { get; init; }
}

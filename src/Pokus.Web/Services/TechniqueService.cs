using Pokus.Web.Data;
using Pokus.Web.Domain;

namespace Pokus.Web.Services;

public interface ITechniqueService
{
    IReadOnlyList<Technique> All { get; }
    Technique? Find(string? slug);
    bool IsFavorite(string slug);
    void ToggleFavorite(string slug);
    IReadOnlyList<Technique> Favorites();
}

public sealed class InMemoryTechniqueService : ITechniqueService
{
    private readonly HashSet<string> favorites = ["active-recall"];

    public IReadOnlyList<Technique> All => TechniqueSeed.All;

    public Technique? Find(string? slug) => All.FirstOrDefault(t => t.Slug == slug);

    public bool IsFavorite(string slug) => favorites.Contains(slug);

    public void ToggleFavorite(string slug)
    {
        if (!favorites.Remove(slug)) favorites.Add(slug);
    }

    public IReadOnlyList<Technique> Favorites() => All.Where(t => favorites.Contains(t.Slug)).ToList();
}

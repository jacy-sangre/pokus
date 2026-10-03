using Pokus.Web.Data;
using Pokus.Web.Domain;

namespace Pokus.Web.Services;

public interface IMessageService
{
    /// <summary>Picks a message in the user's tone, never the same one twice in a row for a category.</summary>
    string Pick(MessageCategory category, int count = 0);
}

public sealed class InMemoryMessageService(ISettingsService settings) : IMessageService
{
    private readonly Dictionary<MessageCategory, string> lastShown = [];
    private readonly Lock gate = new();

    public string Pick(MessageCategory category, int count = 0)
    {
        var options = MessageSeed.All.Where(m => m.Category == category && m.Tone == settings.Tone).Select(m => m.Text).ToList();
        if (options.Count == 0) return "";

        string text;
        lock (gate)
        {
            if (options.Count > 1 && lastShown.TryGetValue(category, out var last)) options.Remove(last);
            text = options[Random.Shared.Next(options.Count)];
            lastShown[category] = text;
        }
        return string.Format(text, count);
    }
}

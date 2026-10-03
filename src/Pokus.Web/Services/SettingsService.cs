using Pokus.Web.Data;
using Pokus.Web.Domain;

namespace Pokus.Web.Services;

public interface ISettingsService
{
    Tone Tone { get; set; }
    FocusPreset DefaultPreset { get; set; }
}

public sealed class InMemorySettingsService : ISettingsService
{
    public Tone Tone { get; set; } = Tone.Witty;
    public FocusPreset DefaultPreset { get; set; } = DemoSeed.Pomodoro;
}

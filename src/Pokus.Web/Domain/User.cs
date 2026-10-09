namespace Pokus.Web.Domain;

public sealed class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; init; }

    public UserSettings Settings { get; set; } = new();
}

public sealed class UserSettings
{
    public Tone Tone { get; set; } = Tone.Witty;
    public string DefaultPresetKey { get; set; } = "pomodoro";
    public string? Goal { get; set; }
}

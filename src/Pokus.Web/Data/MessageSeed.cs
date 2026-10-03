using Pokus.Web.Domain;
using static Pokus.Web.Domain.MessageCategory;
using static Pokus.Web.Domain.Tone;

namespace Pokus.Web.Data;

/// <summary>Message library (spec 5.10, Appendix B). Neutral never jokes; no tone shames the user.</summary>
public static class MessageSeed
{
    public static readonly IReadOnlyList<MessageTemplate> All =
    [
        new(DashboardGreeting, Neutral, "Welcome back. Pick something to start with."),
        new(DashboardGreeting, Neutral, "Good to see you. Here is where things stand."),
        new(DashboardGreeting, Casual, "Hey, welcome back. What are we working on?"),
        new(DashboardGreeting, Casual, "Oh hi. Ready when you are."),
        new(DashboardGreeting, Witty, "Congratulations, you opened the study page. Now the hard part."),
        new(DashboardGreeting, Witty, "Look at you, showing up on purpose. The notes are impressed."),
        new(DashboardGreeting, Filipino, "Welcome back! Tara, aral."),
        new(DashboardGreeting, Filipino, "Uy, nandito ka na. Simulan na natin, kahit konti lang."),
        new(DashboardGreeting, Chaotic, "YOU'RE HERE! That's half the battle. (It's not.)"),
        new(DashboardGreeting, Chaotic, "THE STUDENT HAS ARRIVED. The timer trembles."),

        new(CardsDue, Neutral, "{0} cards are due."),
        new(CardsDue, Neutral, "{0} cards are ready for review."),
        new(CardsDue, Casual, "You've got {0} cards waiting."),
        new(CardsDue, Casual, "{0} cards are up whenever you are."),
        new(CardsDue, Witty, "{0} cards are due. They've been very patient."),
        new(CardsDue, Witty, "{0} cards are waiting. They brought snacks."),
        new(CardsDue, Filipino, "May {0} cards na naghihintay."),
        new(CardsDue, Filipino, "{0} cards, nakapila na. Kaya mo 'yan."),
        new(CardsDue, Chaotic, "{0} CARDS. THEY'RE LINING UP."),
        new(CardsDue, Chaotic, "{0} CARDS HAVE ENTERED THE CHAT."),
    ];
}

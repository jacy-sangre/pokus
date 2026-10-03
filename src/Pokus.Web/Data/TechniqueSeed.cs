using Pokus.Web.Domain;
using static Pokus.Web.Domain.Difficulty;
using static Pokus.Web.Domain.EvidenceLevel;

namespace Pokus.Web.Data;

/// <summary>The 12 techniques seeded for Activity 01/02. Wording is original.</summary>
public static class TechniqueSeed
{
    public static readonly IReadOnlyList<Technique> All =
    [
        new("active-recall", "Active Recall", ["Studying", "Memory", "Exam prep"], ResearchSupported, Easy, "20–40 min",
            "Close the book and pull the answer out of your own head. The retrieving is the practice, not just the check."),
        new("pomodoro", "Pomodoro Technique", ["Focus", "Time management"], CommonlyUsed, Easy, "25/5 min",
            "Work in short timed rounds with a real break between them. The timer does the deciding so you do not have to."),
        new("spaced-repetition", "Spaced Repetition", ["Studying", "Memory"], ResearchSupported, Moderate, "10–20 min daily",
            "Review just before you would forget, with gaps that grow each time you get it right."),
        new("implementation-intentions", "Implementation Intentions", ["Planning", "Productivity"], ResearchSupported, Easy, "2–5 min setup",
            "Decide the when and where in advance: “When it is 7 PM at my desk, I open the problem set.”"),
        new("feynman", "Feynman Technique", ["Studying"], MixedOrLimited, Moderate, "20–40 min",
            "Explain the idea in plain words as if to a beginner, then go back and patch the gaps you hit."),
        new("two-minute-rule", "Two-Minute Rule", ["Productivity", "Planning"], PracticalHeuristic, Easy, "2 min",
            "If a task takes less than two minutes, do it now instead of adding it to a list."),
        new("deep-work", "Deep Work", ["Focus", "Productivity"], CommonlyUsed, Advanced, "60–90 min",
            "Protect one long block for hard, single-minded work with notifications off and a clear goal."),
        new("interleaving", "Interleaving", ["Studying", "Exam prep"], ResearchSupported, Moderate, "30–60 min",
            "Mix different problem types in one session instead of doing twenty of the same kind in a row."),
        new("method-of-loci", "Method of Loci", ["Memory"], ResearchSupported, Advanced, "15–30 min",
            "Place the things you need to remember along a route you know well, then walk it in your head."),
        new("fifty-two-seventeen", "52/17 Method", ["Focus", "Time management"], MixedOrLimited, Easy, "52/17 min",
            "Work for 52 minutes, then step away for 17. A longer rhythm for people who find 25 minutes too short."),
        new("time-blocking", "Time Blocking", ["Planning", "Time management"], CommonlyUsed, Moderate, "Day or week planning",
            "Give every task a slot on the calendar, so the plan for the day is a schedule instead of a wish list."),
        new("cornell-notes", "Cornell Note-Taking", ["Note-taking", "Studying"], MixedOrLimited, Easy, "During class + 10 min",
            "Split the page into notes, cues and a summary, so your notes turn into questions you can quiz yourself with."),
    ];
}

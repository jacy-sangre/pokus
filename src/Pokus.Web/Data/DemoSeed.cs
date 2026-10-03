using Pokus.Web.Domain;

namespace Pokus.Web.Data;

/// <summary>
/// Sample decks, sessions and reviews so the prototype has something to show. Times are relative to app start,
/// so the dashboard looks lived-in whenever it is run. Everything is in memory and resets on restart.
/// </summary>
public static class DemoSeed
{
    public static readonly FocusPreset Pomodoro = new("pomodoro", "Pomodoro", 25, 5);
    public static readonly FocusPreset DeepWork = new("ninety-twenty", "Ultradian", 90, 20);
    public static readonly FocusPreset Long = new("long-focus", "Long", 60, 10);

    public static (List<Subject> Subjects, List<Deck> Decks) Flashcards(DateTimeOffset now)
    {
        var toc = new Subject { Name = "Theory of Computation" };
        var bio = new Subject { Name = "Biology 101" };

        var automata = Deck(toc, "Finite Automata", now,
        [
            ("What are the five parts of a DFA?", "States, an input alphabet, a transition function, a start state, and accept states.", -1),
            ("What makes an automaton deterministic?", "Every state has exactly one transition for each input symbol.", -1),
            ("What language does a DFA accept?", "The set of all strings that finish in an accept state.", 3),
            ("NFA or DFA: which is more powerful?", "Neither. Every NFA has an equivalent DFA.", 7),
            ("What is the epsilon closure of a state?", "Every state you can reach from it using only epsilon moves.", -1),
            ("What does the transition function of a DFA take as input?", "A state and an input symbol.", -1),
            ("Can a DFA have more than one start state?", "No. It has exactly one.", -1),
            ("What is a dead (trap) state?", "A non-accepting state that every transition loops back to.", -1),
            ("How do you turn an NFA into a DFA?", "The subset construction: each DFA state is a set of NFA states.", 5),
            ("What is a regular language?", "A language that some finite automaton accepts.", -1),
            ("What does the pumping lemma help you prove?", "That a language is not regular.", 14),
            ("Why can no DFA recognise aⁿbⁿ?", "It would need unbounded memory to count the a's.", -1),
        ]);

        var regex = Deck(toc, "Regular Expressions", now,
        [
            ("What does the Kleene star mean?", "Zero or more repetitions of the expression before it.", 4),
            ("What is the regex for 'one or more a's'?", "a+ (or aa*).", 6),
            ("What does the union operator | mean?", "Match either the left or the right expression.", 3),
            ("Are regular expressions and DFAs equally powerful?", "Yes. They describe exactly the regular languages.", 9),
            ("What does ε match?", "The empty string.", 2),
            ("What is the regex for strings over {a,b} ending in b?", "(a|b)*b", 12),
        ]);

        var cells = Deck(bio, "Cell Structure", now,
        [
            ("What does the mitochondrion do?", "Produces most of the cell's ATP through cellular respiration.", -1),
            ("Which organelle holds the cell's DNA?", "The nucleus.", 5),
            ("What does the ribosome make?", "Proteins, by translating messenger RNA.", -1),
            ("What is the job of the cell membrane?", "It controls what enters and leaves the cell.", 8),
            ("What do chloroplasts do?", "Carry out photosynthesis in plant cells.", 3),
            ("What does the Golgi apparatus do?", "Modifies, sorts and packages proteins for transport.", -1),
            ("Rough vs smooth ER: what is the difference?", "Rough ER has ribosomes and makes proteins; smooth ER makes lipids.", 11),
            ("What do lysosomes contain?", "Digestive enzymes that break down waste.", -1),
        ]);

        var filipino = Deck(null, "Filipino Vocabulary", now,
        [
            ("Salamat", "Thank you", 0), ("Mahal", "Love, or expensive", 0), ("Bukas", "Tomorrow, or open", 0),
            ("Gutom", "Hungry", 0), ("Aral", "Study, lesson", 0), ("Tubig", "Water", 0),
        ]);
        filipino.UseSpacedRepetition = false;

        return ([toc, bio], [automata, regex, cells, filipino]);
    }

    /// <param name="cards">Front, back, and due offset in days (negative = due now).</param>
    private static Deck Deck(Subject? subject, string name, DateTimeOffset now, (string Front, string Back, int DueInDays)[] cards)
    {
        var deck = new Deck { Name = name, SubjectId = subject?.Id };
        foreach (var (front, back, days) in cards)
        {
            deck.Cards.Add(new Flashcard
            {
                Front = front,
                Back = back,
                Box = days <= 0 ? 0 : Math.Min(5, days / 3 + 1),
                DueAt = days < 0 ? now.AddHours(-2) : now.AddDays(days),
            });
        }
        return deck;
    }

    /// <summary>A Pomodoro already running, plus a few days of finished sessions.</summary>
    public static (FocusSession Active, List<FocusSession> History) Sessions(DateTimeOffset now)
    {
        var active = new FocusSession
        {
            Preset = Pomodoro,
            TechniqueSlug = "pomodoro",
            Task = "Chapter 4 problem set",
            StartedAt = now - TimeSpan.FromMinutes(6) - TimeSpan.FromSeconds(18),
        };

        var today = new DateTimeOffset(now.Date, now.Offset);
        var history = new List<FocusSession>
        {
            Done(Pomodoro, "pomodoro", "Chapter 4 problem set", now.AddMinutes(-50), 25),
            Done(Pomodoro, "feynman", "Explain pumping lemma", now.AddMinutes(-140), 20),
            Done(Long, "deep-work", "Lab report outline", today.AddDays(-1).AddHours(19), 60),
            Done(Pomodoro, "active-recall", "Cell structure review", today.AddDays(-1).AddHours(15), 25),
            Done(Pomodoro, "active-recall", "DFA flashcards", today.AddDays(-2).AddHours(20), 30),
            Done(DeepWork, "deep-work", "Essay draft", today.AddDays(-2).AddHours(9), 40),
            Done(Pomodoro, "pomodoro", "Reading: chapter 3", today.AddDays(-6).AddHours(18), 25),
            Done(Pomodoro, "pomodoro", "Problem set 3", today.AddDays(-8).AddHours(18), 50),
        };
        return (active, history.OrderByDescending(s => s.StartedAt).ToList());
    }

    private static FocusSession Done(FocusPreset preset, string technique, string task, DateTimeOffset start, int minutes) => new()
    {
        Preset = preset,
        TechniqueSlug = technique,
        Task = task,
        StartedAt = start,
        EndedAt = start.AddMinutes(minutes),
        Status = SessionStatus.Completed,
    };

    /// <summary>Review history over the last few days, roughly 78% "knew it".</summary>
    public static List<CardReview> Reviews(IEnumerable<Deck> decks, DateTimeOffset now)
    {
        var cards = decks.SelectMany(d => d.Cards).ToList();
        var reviews = new List<CardReview>();
        int[] perDay = [12, 20, 14]; // two days ago, yesterday, today
        for (var day = 0; day < perDay.Length; day++)
        {
            var date = now.AddDays(day - 2).AddHours(-1);
            for (var i = 0; i < perDay[day]; i++)
            {
                reviews.Add(new CardReview(cards[(day * 7 + i) % cards.Count].Id, date.AddMinutes(-i), i % 9 is not (2 or 6)));
            }
        }
        return reviews;
    }
}

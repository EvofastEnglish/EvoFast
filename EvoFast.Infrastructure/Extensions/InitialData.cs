using EvoFast.Domain.Models;
using EvoFast.Domain.ValueObjects;

namespace EvoFast.Infrastructure.Extensions;

public class InitialData
{
    public static IEnumerable<WordSet> WordSets => new List<WordSet>
    {
        WordSet.Create(Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"), 1),
        WordSet.Create(Guid.Parse("0194e577-3cc8-7b3d-b008-67bd2be0d14c"), 2)
    };
    
    public static IEnumerable<Question> Questions => new List<Question>
    {
        new Question
        {
            Id = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            WordSetId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "分析",
        },
        new Question
        {
            Id = Guid.Parse("0194e577-3cc8-7b3d-b008-67bd2be0d14c"),
            WordSetId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "品質",
        },
        new Question
        {
            Id = Guid.Parse("0194e577-6bb7-729d-929b-3b3760b536b5"),
            WordSetId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "製造",
        },
        new Question
        {
            Id = Guid.Parse("0194cf4f-b787-77d4-b222-d5c803eb5bb9"),
            WordSetId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "問題",
        },
        
    };

    public static IEnumerable<Answer> Answers => new List<Answer>
    {
        new Answer
        {
            Id = Guid.Parse("0194cfee-0fd8-7bce-99d3-a6a777a6bb84"),
            QuestionId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "Analysis",
            TranslatedName = "分析",
            IsCorrect = true,
        },
        new Answer
        {
            Id = Guid.Parse("0194d527-15a6-73d1-8b35-6182357d59d4"),
            QuestionId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "Transaction",
            TranslatedName = "取引",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d606-96b4-7c0a-9f27-d213a7bba9ad"),
            QuestionId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "Manufacturing",
            TranslatedName = "製造",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d606-e8d9-71c7-b356-018aed6cda07"),
            QuestionId = Guid.Parse("0194d661-81b1-7eb9-9542-b22efedf0d78"),
            Name = "Profit",
            TranslatedName = "利益",
            IsCorrect = false,
        },
        
        new Answer
        {
            Id = Guid.Parse("0194d607-2d34-78c6-8ed6-0c8b7031c928"),
            QuestionId = Guid.Parse("0194e577-3cc8-7b3d-b008-67bd2be0d14c"),
            Name = "Budget",
            TranslatedName = "予算",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d607-60d4-7cb1-b5f6-536819b34c78"),
            QuestionId = Guid.Parse("0194e577-3cc8-7b3d-b008-67bd2be0d14c"),
            Name = "Transaction",
            TranslatedName = "取引",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d607-caaf-7776-bcad-6c7fef44ec7d"),
            QuestionId = Guid.Parse("0194e577-3cc8-7b3d-b008-67bd2be0d14c"),
            Name = "Quality",
            TranslatedName = "品質",
            IsCorrect = true,
        },
        new Answer
        {
            Id = Guid.Parse("0194d609-16d2-7b53-84eb-9fceb6ce16a0"),
            QuestionId = Guid.Parse("0194e577-3cc8-7b3d-b008-67bd2be0d14c"),
            Name = "Documents",
            TranslatedName = "資料",
            IsCorrect = false,
        },
        
        new Answer
        {
            Id = Guid.Parse("0194d609-5413-7e00-8db7-783a3ade2cd1"),
            QuestionId = Guid.Parse("0194e577-6bb7-729d-929b-3b3760b536b5"),
            Name = "Budget",
            TranslatedName = "予算",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d609-bf0b-74ff-a243-80864a2aa6a2"),
            QuestionId = Guid.Parse("0194e577-6bb7-729d-929b-3b3760b536b5"),
            Name = "Quality",
            TranslatedName = "品質",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d609-f1dd-7c11-b576-1637d683f159"),
            QuestionId = Guid.Parse("0194e577-6bb7-729d-929b-3b3760b536b5"),
            Name = "Efficiency",
            TranslatedName = "効率",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d60a-91b9-72ff-9bd1-187ef14305e7"),
            QuestionId = Guid.Parse("0194e577-6bb7-729d-929b-3b3760b536b5"),
            Name = "Manufacturing",
            TranslatedName = "製造",
            IsCorrect = true,
        },
        
        new Answer
        {
            Id = Guid.Parse("0194d60b-20fd-7a09-bcb7-6b66be688d65"),
            QuestionId = Guid.Parse("0194cf4f-b787-77d4-b222-d5c803eb5bb9"),
            Name = "Problem",
            TranslatedName = "問題",
            IsCorrect = true,
        },
        new Answer
        {
            Id = Guid.Parse("0194d60b-567c-7dc8-ab0e-8e67eadd45ce"),
            QuestionId = Guid.Parse("0194cf4f-b787-77d4-b222-d5c803eb5bb9"),
            Name = "Sales",
            TranslatedName = "売上",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d60b-9335-7d37-8d99-36acb430468c"),
            QuestionId = Guid.Parse("0194cf4f-b787-77d4-b222-d5c803eb5bb9"),
            Name = "Documents",
            TranslatedName = "資料",
            IsCorrect = false,
        },
        new Answer
        {
            Id = Guid.Parse("0194d60b-c0a9-7bf3-a4a8-cf0c97a05207"),
            QuestionId = Guid.Parse("0194cf4f-b787-77d4-b222-d5c803eb5bb9"),
            Name = "Training",
            TranslatedName = "研修",
            IsCorrect = false,
        },
    };
}
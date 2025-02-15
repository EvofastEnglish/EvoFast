namespace EvoFast.Domain.ValueObjects;

public record WordSetId
{
    public Guid Value { get; }
    private WordSetId(Guid value) => Value = value;

    public static WordSetId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("WordSetId cannot be empty");
        }

        return new WordSetId(value);
    }
}
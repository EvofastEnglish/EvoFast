namespace EvoFast.Domain.ValueObjects;

public class WordSetName
{ 
    public string Value { get; }
    private WordSetName(string value) => Value = value;
    
    public static WordSetName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        return new WordSetName(value);
    }
}
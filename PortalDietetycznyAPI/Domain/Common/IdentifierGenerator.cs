namespace PortalDietetycznyAPI.Domain.Common;

public abstract class IdentifierGenerator
{
    private static readonly DateTimeOffset StartDate = new DateTimeOffset(new DateTime(2024, 1, 1), TimeSpan.Zero);

    protected int GenerateUid()
    {
        var now = DateTimeOffset.UtcNow;
        var unixTimeSeconds =(int)(now - StartDate).TotalSeconds;

        return unixTimeSeconds;
    }

    protected string GenerateUrl(int uid, string name)
    {
        name = name.ToLower();
        
        var polishLettersDict = new Dictionary<char, char>
        {
            {'ą', 'a'},
            {'ć', 'c'},
            {'ę', 'e'},
            {'ł', 'l'},
            {'ń', 'n'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ź', 'z'},
            {'ż', 'z'}
        };

        foreach (var (key, value)  in polishLettersDict)
        {
            name = name.Replace(key, value);
        }
        
        var url = name.Replace(' ', newChar: '-')+ "-" + uid.ToString("X");

        return url;
    }
}
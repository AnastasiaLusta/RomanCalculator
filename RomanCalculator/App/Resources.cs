namespace RomanCalculator.App;

public class Resources
{
    public string Culture { get; set; } = "uk-UA";

    public string GetEmptyStringMessage(string? culture = null)   // message for empty string exception
        => (culture ?? Culture) switch
        {
            "uk-UA" => "Порожній рядок неприпустимий",
            "en-US" => "Empty string not allowed",
            _ => throw new Exception()
        };

    public string GetInvalidCharMessage(char c, string? culture = null)   // message for invalid char exception
        => (culture ?? Culture) switch
        {
            "uk-UA" => $"Недозволений символ '{c}'",
            "en-US" => $"Invalid char '{c}'",
            _ => throw new Exception("Unupported culture")
        };

    public string
        GetInvalidTypeMessage(int objNumber, string type, string? culture = null)   // message for invalid type exception
        => (culture ?? Culture) switch
        {
            "uk-UA" => $"obj{objNumber}: тип '{type}' не підтримується",
            "en-US" => $"obj{objNumber}: type '{type}' unsupported",
            _ => throw new Exception("Unupported culture")
        };

    public string GetMispalcedNMessage(string? culture = null)   // message for misplaced N exception
        => (culture ?? Culture) switch
        {
            "uk-UA" => "'N' не дозволяється у даному контексті",
            "en-US" => "'N' is not allowed in this context",
            _ => throw new Exception("Unupported culture")
        };


    public string GetEnterNumberMessage(string? culture = null)   // message for user to enter number
        => (culture ?? Culture) switch
        {
            "uk-UA" => "Введiть число: ",
            "en-US" => "Enter number: ",
            _ => throw new Exception("Unsupported culture"),
        };

    public string GetEnterOperationMessage(string? culture = null)   //message for user to enter operation
        => (culture ?? Culture) switch
        {
            "uk-UA" => "Введiть операцiю: ",
            "en-US" => "Enter operation: ",
            _ => throw new Exception("Unsupported culture"),
        };

    public string GetResultMessage(int res, string? culture = null)   // message for result
        => (culture ?? Culture) switch
        {
            "uk-UA" => $"Результат: {res}",
            "en-US" => $"Result: {res}",
            _ => throw new Exception("Unsupported culture"),
        };
}
namespace Newsletter.Core;

public static class Configuration
{
    public static string Rootpath {get; set;} = string.Empty;
    public static OpenAiConfiguration OpenAi { get; set; } = new();

    public class OpenAiConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
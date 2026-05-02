using Newsletter.Ai.Provider.Abstractions;

namespace Newsletter.Ai.Provider;

public class FilePromptProvider : IPromptProvider
{
    public async Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken)
    {
        var assembly = typeof(FilePromptProvider).Assembly;
        
        var resourceName = $"Newsletter.Ai.Prompts.{agentName}.md";
        await using var stream = assembly.GetManifestResourceStream(resourceName);
        
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(cancellationToken);
    }
}
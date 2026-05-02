namespace Newsletter.Ai.Provider.Abstractions;

public interface IPromptProvider
{
    Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken);
}
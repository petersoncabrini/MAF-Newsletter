using System.Text.Json;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Ai.Models.AiModels;
using Newsletter.Ai.Provider.Abstractions;
using Newsletter.Core;
using Newsletter.Core.Agents.Abstractions;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using OpenAI;
using OpenAI.Chat;

namespace Newsletter.Ai.Agents;

public class TitleCreatorAgent(
    ILogger<TitleCreatorAgent> logger,
    [FromKeyedServices(PromptProvider.File)] IPromptProvider promptProvider
    ) : IAgent<IEnumerable<Article>, string>
{
    private const string AgentName = "TitleCreatorAgent";
    private const string Prompt = "Create a newsletter title content based on this JSON:";
    private const float Temperature = 0.7f;
    
    public async Task<string> RunAsync(IEnumerable<Article> data, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting newsletter title creating process...");
        
        //TODO - Singleton
        var client = new OpenAIClient(Configuration.OpenAi.ApiKey);
        
        var instructions = await promptProvider.GetPromptAsync(AgentName, cancellationToken);

        var agent = client.GetChatClient(AiModels.Gpt4OMini)
            .AsAIAgent(new ChatClientAgentOptions()
            {
                Name = AgentName,
                Description = "An agent that creates newsletter title based on a list of articles.",
                ChatOptions = new ChatOptions()
                {
                    ModelId = AiModels.Gpt4OMini,
                    Temperature = Temperature,
                    Instructions = instructions
                } 
            });
        
        var prompt = $"{Prompt} {JsonSerializer.Serialize(data)}";
        var response = await agent.RunAsync<string>(prompt, cancellationToken: cancellationToken);
        
        logger.LogInformation("Title creating process completed.");

        return response.Result;
    }
}
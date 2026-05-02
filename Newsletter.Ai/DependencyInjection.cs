using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Ai.Agents;
using Newsletter.Ai.Provider;
using Newsletter.Ai.Provider.Abstractions;
using Newsletter.Core.Agents.Abstractions;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;

namespace Newsletter.Ai;

public static class DependencyInjection
{
    public static IServiceCollection AddAgents(this IServiceCollection services)
    {
        services.AddKeyedTransient<IAgent<IEnumerable<Article>, string>, TitleCreatorAgent>(AgentType
            .TitleCreatorAgent);
        services.AddKeyedTransient<IAgent<IEnumerable<Article>, string>, NewsletterWriterAgent>(AgentType
            .NewsletterWriterAgent);
        
        services.AddKeyedTransient<IPromptProvider, FilePromptProvider>(PromptProvider.File);

        return services;
    }
}
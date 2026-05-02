using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Core.Agents.Abstractions;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;
using Newsletter.Core.Services.Abstractions;

namespace Newsletter.Infra.Services;

public class NewsletterService(
    ILogger<NewsletterService> logger,
    IArticleRepository articleRepository,
    ISubscriberRepository subscriberRepository,
    IEmailService emailService,
    
    [FromKeyedServices(AgentType.TitleCreatorAgent)]
    IAgent<IEnumerable<Article>, string> titleCreatorAgent,
    
    [FromKeyedServices(AgentType.NewsletterWriterAgent)]
    IAgent<IEnumerable<Article>, string> newsletterWriterAgent
    
    ) : INewsletterService
{
    public async Task SendAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting week posts...");
        var posts = await articleRepository.GetFromLastWeekAsync(cancellationToken);
        if (!posts.Any())
            return;
        
        logger.LogInformation("Creating newsletter title...");
        var subject = await titleCreatorAgent.RunAsync(posts, cancellationToken);
        
        logger.LogInformation("Writing newsletter content...");
        var body = await newsletterWriterAgent.RunAsync(posts, cancellationToken);
        
        logger.LogInformation("Getting subscribers...");
        var subscribers = await subscriberRepository.GetAllAsync(cancellationToken);
        
        logger.LogInformation("Sending emails...");
        foreach (var subscriber in subscribers)        {
            await emailService.SendAsync(subscriber.Email, subject, body, cancellationToken);
        }
        
        logger.LogInformation("Process Completed...");
    }
}
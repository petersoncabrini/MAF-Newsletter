using Newsletter.Core.Services.Abstractions;

namespace Newsletter.Api.Workers;

public class NewsletterWorker(
    ILogger<NewsletterWorker> logger,
    IServiceScopeFactory scopeFactory
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("NewsletterWorker is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = GetNextSundayAtEight(now);
            
            //var delay = nextRun - now;
            var delay = TimeSpan.FromSeconds(10); // For testing, run every 10 seconds
            
            logger.LogInformation($"Next newsletter generation scheduled for: {nextRun}");

            try
            {
                await Task.Delay(delay, stoppingToken);
                logger.LogInformation("Starting newsletter generation...");
                await DoWorkAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            
        }
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("NewsletterWorker is doing work.");
        using var scope = scopeFactory.CreateScope();
        var newsletterService = scope.ServiceProvider.GetRequiredService<INewsletterService>();
        await newsletterService.SendAsync(cancellationToken);
    }

    private DateTime GetNextSundayAtEight(DateTime current)
    {
        var daysUntilSunday = ((int)DayOfWeek.Sunday - (int)current.DayOfWeek + 7) % 7;
        var nextSunday = current.AddDays(daysUntilSunday).Date.AddHours(8);
        if (nextSunday <= current)        
            nextSunday = nextSunday.AddDays(7);
        
        return nextSunday;
    }
}
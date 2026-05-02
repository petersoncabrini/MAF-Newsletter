using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;

namespace Newsletter.Infra.Repositories;

public class SubscriberRepository : ISubscriberRepository
{
    public async Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken)
    {
        return
        [
            new Subscriber("Person 1", "person1@email.com"),
            new Subscriber("Person 2", "person2@email.com")
        ];
    }
}
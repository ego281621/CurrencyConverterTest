using CurrencyConverterTest.Core.Entities;

namespace CurrencyConverterTest.Repository.RequestLogRepository
{

    // Implements IRequestLogRepository interface
    public class RequestLogRepository : IRequestLogRepository
    {

        // DB context dependency injection
        private readonly CurrencyTestDbContext context;

        // Constructor injects DB context
        public RequestLogRepository(CurrencyTestDbContext context)
        {
            this.context = context;
        }

        // Implementation of AddRequestLog method
        public async Task AddRequestLog(RequestLog requestLog)
        {
            // Add entity to context
            context.RequestLogs.Add(requestLog);

            // Save changes
            await context.SaveChangesAsync();
        }

    }

}
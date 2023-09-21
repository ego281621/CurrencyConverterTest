using CurrencyConverterTest.Core.Entities;

namespace CurrencyConverterTest.Repository.RequestLogRepository
{

    // Interface for request log repository
    public interface IRequestLogRepository
    {

        // Asynchronously adds a new request log entry
        Task AddRequestLog(RequestLog requestLog);

    }

}
using CurrencyConverterTest.Models;

namespace CurrencyConverterTest.Services.CurrencyService
{
    public interface ICurrencyService
    {

        Task<RatesResponse> GetLatestRateListCurrency();
        Task<RatesResponse> GetLatestRateFromToCurrency(string from, string to);
        Task<CurrencyConvertResponse> Convert(CurrencyConvertRequest request);

    }

}
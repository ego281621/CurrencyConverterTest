
namespace CurrencyConverterTest.Models
{

    // Model to represent currency conversion response
    public class CurrencyConvertResponse
    {

        // Converted amount in destination currency
        public decimal ConvertedAmount { get; set; }

        // Destination currency code (ISO code)
        public string ToCurrency { get; set; }

    }

}
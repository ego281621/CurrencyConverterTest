namespace CurrencyConverterTest.Models
{

    // Model to represent currency conversion request
    public class CurrencyConvertRequest
    {

        // Source currency code (ISO code)
        public string? FromCurrency { get; set; }

        // Amount to convert
        public decimal Amount { get; set; }

        // Destination currency code (ISO code)
        public string? ToCurrency { get; set; }

    }

}
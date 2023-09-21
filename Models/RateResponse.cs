
namespace CurrencyConverterTest.Models
{

    // Model to represent response containing currency rates
    public class RatesResponse
    {

        // Dictionary of currency code to rate object
        public Dictionary<string, Rate> Data { get; set; }

    }

    // Model to represent a single currency rate
    public class Rate
    {

        // Currency code (ISO code)
        public string Code { get; set; }

        // Exchange rate value
        public decimal Value { get; set; }

    }

}
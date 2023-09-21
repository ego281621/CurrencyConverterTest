
namespace CurrencyConverterTest.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public Errors Errors { get; set; }
        public string Info { get; set; }
    }

    public class Errors
    {
        public List<string> Currencies { get; set; }
    }
}
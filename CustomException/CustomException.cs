using System.ComponentModel.DataAnnotations;

namespace CurrencyConverterTest.CustomException
{

    // Custom exception for currency not found
    public class CurrencyNotFoundException : Exception
    {
        public CurrencyNotFoundException(string currencyCode)
          : base($"Currency '{currencyCode}' not found")
        {
        }
    }

    // Custom exception when currency list not found
    public class CurrencyListNotFoundException : Exception
    {
        public CurrencyListNotFoundException()
          : base($"Currency List not found")
        {
        }
    }

    // Custom exception for invalid request data
    public class InvalidRequestDataException : Exception
    {
        public InvalidRequestDataException(string message)
          : base(message)
        {
        }
    }

    public class NoApiKeyException : Exception
    {
        public NoApiKeyException(string message)
          : base(message)
        {
        }
    }

    public class CurrencyAPIErrorException : Exception
    {
        public CurrencyAPIErrorException(string message)
          : base(message)
        {
        }
    }

}
// Namespace imports
using CurrencyConverterTest.CustomException;
using CurrencyConverterTest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;

namespace CurrencyConverterTest.Services.CurrencyService
{

    // Class implements interface
    public class CurrencyService : ICurrencyService
    {

        // Dependency injection 
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string currencyApiSetting = "CurrencyApiSettings:ApiKeyValue";

        // Constructor injection
        public CurrencyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Get latest rates for all currencies
        public async Task<RatesResponse> GetLatestRateListCurrency()
        {
            // API key
            string apiKey = _configuration.GetValue<string>(currencyApiSetting);

            //Throw error if no api key
            if(string.IsNullOrEmpty(apiKey))
            {
                throw new NoApiKeyException("No Currency API Key to get data.");
            }

            // API URL
            string url = "https://api.currencyapi.com/v3/latest";

            // Create REST client
            RestClient client = new RestClient(url);

            // Create request
            RestRequest request = new RestRequest(url, Method.Get);

            // Add API key header
            request.AddHeader("apikey", apiKey);

            // Call API and get response
            RestResponse response = await client.ExecuteAsync(request);

            // Get JSON content from response
            string json = response.Content;

            // Validate response 
            if (string.IsNullOrEmpty(json))
                throw new CurrencyListNotFoundException();

            // Deserialize JSON to rates response object
            RatesResponse ratesResponse = JsonConvert.DeserializeObject<RatesResponse>(json);

            // Return response
            return ratesResponse;

        }

        public async Task<RatesResponse> GetLatestRateFromToCurrency(string from, string to)
        {
            // API key
            string apiKey = _configuration.GetValue<string>(currencyApiSetting);

            //Throw error if no api key
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new NoApiKeyException("No Currency API Key to get data.");
            }

            // API URL
            var url = $"https://api.currencyapi.com/v3/latest?apikey={apiKey}&base_currency={from}&currencies={to}";

            // Create REST client
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("apikey", apiKey);

            // Call API and get response
            RestResponse response = await client.ExecuteAsync(request);
            var json = response.Content;

            if (string.IsNullOrEmpty(json))
                throw new CurrencyListNotFoundException();

            // Get JSON content from response
            if (response.IsSuccessful)
            {
                // Deserialize JSON to rates response object  and return respnse
                return JsonConvert.DeserializeObject<RatesResponse>(json);
            }
            else
            {
                throw new CurrencyAPIErrorException(json);
            }

        }

        public async Task<CurrencyConvertResponse> Convert(CurrencyConvertRequest request)
        {

            // Get rate for requested currencies 
            var rateResult = await GetLatestRateFromToCurrency(request.FromCurrency, request.ToCurrency);

            // Validate rate result
            if (rateResult == null)
                throw new CurrencyNotFoundException(request.FromCurrency);

            // Validate from currency 
            if (string.IsNullOrEmpty(request.FromCurrency))
                throw new InvalidRequestDataException("From currency is required");

            // Validate amount
            if (request.Amount <= 0)
                throw new InvalidRequestDataException("Amount must be greater than zero");

            // Get rate from result
            var rateData = rateResult.Data.FirstOrDefault();

            // Set rate or default to 0
            decimal rate = rateData.Value != null ? rateData.Value.Value : 0;

            // Calculate converted amount
            var amount = request.Amount * rate;

            // Return response with converted amount
            return new CurrencyConvertResponse
            {
                ConvertedAmount = amount,
                ToCurrency = request.ToCurrency
            };

        }

    }

}
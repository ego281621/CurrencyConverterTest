// Namespace for custom exceptions, models, and services
using CurrencyConverterTest.CustomException;
using CurrencyConverterTest.Models;
using CurrencyConverterTest.Services.CurrencyService;

// ASP.NET Core namespaces
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterTest.Controllers
{

    // API controller attribute 
    [ApiController]

    // Controller route
    [Route("[controller]")]

    //Auhtorize API
    [Authorize]
    // Inherit from base controller class
    public class CurrencyController : ControllerBase
    {

        // Dependency injection for currency service
        private readonly ICurrencyService _currencyService;

        // Dependency injection for logging
        private readonly ILogger<CurrencyController> _logger;

        // Constructor injects dependencies
        public CurrencyController(ICurrencyService currencyService, ILogger<CurrencyController> logger)
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        // GET rates endpoint
        [HttpGet("rates")]
        public async Task<IActionResult> GetRates()
        {
            try
            {
                // Call service method  
                var response = await _currencyService.GetLatestRateListCurrency();

                // Return OK response
                return Ok(response);
            }
            catch (CurrencyListNotFoundException ex)
            {
                // Return NotFound for custom exception
                return NotFound(ex.Message);
            }
            catch (NoApiKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log general exceptions
                _logger.LogError(ex, "Error get list currency");

                // Return Bad Request
                return BadRequest(ex);
            }
        }

        // POST endpoint to convert currency
        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] CurrencyConvertRequest request)
        {
            try
            {
                // Call service method
                var response = await _currencyService.Convert(request);

                // Return OK response
                return Ok(response);
            }
            catch (CurrencyNotFoundException ex)
            {
                // Return NotFound for custom exception
                return NotFound(ex.Message);
            }
            catch (InvalidRequestDataException ex)
            {
                // Return Bad Request for custom exception  
                return BadRequest(ex.Message);
            }
            catch (NoApiKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CurrencyAPIErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {_logger.LogError(ex, "Error converting currency");

                // Return Bad Request
                // Log general exceptions
                
                return BadRequest(ex);
            }
        }

    }

}
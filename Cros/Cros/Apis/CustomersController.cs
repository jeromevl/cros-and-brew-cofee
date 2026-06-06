using Cros.BusinessLogic.Dtos;
using Cros.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cros.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetByIdAsync(int customerId)
        {
            IActionResult result;

            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);
                if (customer != null)
                    result = Ok(customer);
                else
                    result = NoContent();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? searchText)
        {
            IActionResult result;

            try
            {
                var results = await _customerService.GetCustomersAsync(pageNumber, pageSize, searchText ?? "");
                result = Ok(results);
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CustomerDto newCustomer)
        {
            IActionResult result;

            try
            {
                await _customerService.CreateCustomerAsync(newCustomer);
                result = Ok();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] CustomerDto existingCustomer)
        {
            IActionResult result;

            try
            {
                await _customerService.UpdateCustomerAsync(existingCustomer);
                result = Ok();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }

        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> DeleteAsync(int customerId)
        {
            IActionResult result;

            try
            {
                await _customerService.DeleteCustomerAsync(customerId, true);
                result = Ok();
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return result;
        }
    }
}
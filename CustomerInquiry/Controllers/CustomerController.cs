using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerInquiry.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerProvider _provider;
        public CustomerController(ICustomerProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Get all Customers.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/api/customers
        ///
        /// </remarks>
        /// <returns code="200">A list of Customers</returns>

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtOs = await _provider.GetAllCustomers();
            return Ok(dtOs);
        }

        /// <summary>
        /// Get a specific customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/api/customers/{id}?includeRelatedEntities=true
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="includeRelatedEntities"></param>
        /// <returns code="200">Get Successfull (Success Status Code)</returns>
        /// <response code="400">If the CustomerDTO based on the customerId could not be found</response> 
        /// 
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int id, bool includeRelatedEntities = false)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _provider.GetCustomer(id, includeRelatedEntities);
            if (item == null) return NotFound();//404 Not Found (Client Error Status Code)
            return Ok(item);//Get Successfull (Success Status Code)
        }

        /// <summary>
        /// Creates a new Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/api/customers
        ///     {
        ///        "CustomerName": "string",
        ///        "ContactEmail": "string",
        ///        "MobileNo": "1234567890"
        ///     }
        ///
        /// </remarks>
        /// <param name="dto"></param>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">If the dto is null or the ModelState is invalid</response> 

        [HttpPost]
        public IActionResult Post([FromBody]CustomerBaseDto dto)
        {
            if (dto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _provider.AddCustomer(dto);
            return result == null ? StatusCode(500, "A problem occurred while handling your request.")
                : CreatedAtRoute("GetCustomer", new { id = result.CustomerId }, result);
        }

        /// <summary>
        /// Delete a Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/api/customers/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">No Content, for delete usually, successfull request that shouldn't return anything</response>
        /// <response code="400">If the CustomerDTO based on the customerId could not be found</response> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _provider.DeleteCustomer(id);
            if (!result.Value) return NotFound();
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return NoContent();
        }
    }
}

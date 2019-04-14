using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerInquiry.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private ICustomerProvider _provider;
        public CustomerController(ICustomerProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var DTOs = await _provider.GetAllCustomers();
            return Ok(DTOs);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int id, bool includeRelatedEntities = false)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _provider.GetCustomer(id, includeRelatedEntities);
            if (item == null) return NotFound();//404 Not Found (Client Error Status Code)
            return Ok(item);//Get Successfull (Success Status Code)
        }

        [HttpPost]
        public IActionResult Post([FromBody]CustomerBaseDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _provider.AddCustomer(DTO);
            if(result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return CreatedAtRoute("GetCustomer", new { id = result.CustomerID }, result);
        }

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

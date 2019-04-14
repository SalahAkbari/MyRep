using AutoMapper;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerInquiry.Controllers
{
    [Route("api/customers")]
    public class TransactionController : Controller
    {
        private ITransactionProvider _provider;
        public TransactionController(ITransactionProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("{customerId}/transaction")]
        public async Task<IActionResult> Get(int customerId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var DTOs = await _provider.GetAllTransactions(customerId);
            return Ok(DTOs);
        }

        [HttpGet("{customerId}/transaction/{id}", Name = "GetTransaction")]
        public async Task<IActionResult> Get(int customerId, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _provider.GetTransaction(customerId, id);
            if (item == null) return NotFound();//404 Not Found (Client Error Status Code)
            return Ok(item);//Get Successfull (Success Status Code)
        }

        //You might wonder why the ids are sent in as separate parameters as opposed to 
        //sending them with the request body and receive it in the Transaction object. 
        //The reason is, ids should be passed into the action with the URL to follow the
        //REST standard. If you do decide to send in the ids with the Transaction object as well,
        //you should check that they are the same as the ones in the URL before taking any action.

        [HttpPost("{customerId}/transaction")]
        public IActionResult Post(int customerId, [FromBody]TransactionBaseDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _provider.AddTransaction(customerId, DTO);
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return CreatedAtRoute("GetTransaction", new { id = result.TransactionID }, result);
        }


        [HttpDelete("{customerId}/transaction/{id}")]
        public async Task<IActionResult> Delete(int customerId, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _provider.DeleteTransaction(id);
            if (!result.Value) return NotFound();
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return NoContent();
        }
    }
}

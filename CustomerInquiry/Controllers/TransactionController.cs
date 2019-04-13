using AutoMapper;
using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CustomerInquiry.Controllers
{
    [Route("api/customers")]
    public class TransactionController : Controller
    {
        IGenericEFRepository<Transaction> _rep;
        public TransactionController(IGenericEFRepository<Transaction> rep)
        {
            _rep = rep;
        }

        [HttpGet("{customerId}/transaction")]
        public IActionResult Get(int customerId)
        {
            var items = _rep.Get().Where(b => b.CustomerId.Equals(customerId));
            var DTOs = Mapper.Map<IEnumerable<TransactionDTO>>(items);
            return Ok(DTOs);
        }

        [HttpGet("{customerId}/transaction/{id}", Name = "GetTransaction")]
        public IActionResult Get(int customerId, int id)
        {
            var item = _rep.Get(id);
            if (item == null || !item.CustomerId.Equals(customerId)) return NotFound();//404 Not Found (Client Error Status Code)
            var DTO = Mapper.Map<TransactionDTO>(item);
            return Ok(DTO);//Get Successfull (Success Status Code)
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
            var itemToCreate = Mapper.Map<Transaction>(DTO);
            itemToCreate.CustomerId = customerId;
            _rep.Add(itemToCreate);
            if (!_rep.Save()) return StatusCode(500, "A problem occurred while handling your request.");
            var createdDTO = Mapper.Map<TransactionDTO>(itemToCreate);
            return CreatedAtRoute("GetTransaction", new { id = createdDTO.TransactionID }, createdDTO);
        }


        [HttpDelete("{customerId}/transaction/{id}")]
        public IActionResult Delete(int customerId, int id)
        {
            if (!_rep.Exists(id)) return NotFound();
            var entityToDelete = _rep.Get(id);
            _rep.Delete(entityToDelete);
            if (!_rep.Save()) return StatusCode(500,
            "A problem occurred while handling your request.");
            return NoContent();
        }
    }
}

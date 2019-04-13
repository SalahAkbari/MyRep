using AutoMapper;
using CustomerInquiry.DataAccess;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerInquiry.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        IGenericEFRepository<Customer> _rep;
        public CustomerController(IGenericEFRepository<Customer> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var item = _rep.Get();
            var DTOs = Mapper.Map<IEnumerable<CustomerDTO>>(item);
            return Ok(DTOs);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id, bool includeRelatedEntities = false)
        {
            var item = _rep.Get(id, includeRelatedEntities);
            if (item == null) return NotFound();//404 Not Found (Client Error Status Code)
            var DTO = Mapper.Map<CustomerDTO>(item);
            return Ok(DTO);//Get Successfull (Success Status Code)
        }

        [HttpPost]
        public IActionResult Post([FromBody]CustomerBaseDTO DTO)
        {
            if (DTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var itemToCreate = Mapper.Map<Customer>(DTO);
            _rep.Add(itemToCreate);
            if (!_rep.Save())
                return StatusCode(500, "A problem occurred while handling your request.");
            var customerDTO = Mapper.Map<CustomerDTO>(itemToCreate);
            return CreatedAtRoute("GetCustomer", new { id = customerDTO.CustomerID }, customerDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.Interface;
using Projekt.Models;
using System;
using System.Collections.Generic;

namespace Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodiController : ControllerBase
    {
        private readonly IProizvodiRepository _proizvodiRepository;

        public ProizvodiController(IProizvodiRepository proizvodiRepository)
        {
            _proizvodiRepository = proizvodiRepository;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            try
            {
                return Ok(_proizvodiRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            try
            {
                var result = _proizvodiRepository.GetProductById(id);
                if(result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutProduct(int id, Product product)
        {
            try
            {
                if(id != product.Id)
                {
                    return BadRequest("Product ids don't match!");
                }

                var productForUpdating = _proizvodiRepository.GetProductById(id);
                if(productForUpdating == null)
                {
                    return NotFound($"Product with id = {id} not found!");
                }

                var updateProduct = _proizvodiRepository.UpdateProduct(product);
                return Ok(updateProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from server");
            }
        }
    }
}

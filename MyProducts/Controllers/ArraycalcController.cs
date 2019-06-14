using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProducts.Models;

namespace MyProducts.Controllers
{
    [Route("api/arraycalc")]
    [ApiController]
    public class ArraycalcController : ControllerBase
    {
        private IArrayCalc _arrayCalc;

        public ArraycalcController(IArrayCalc arrayCalc)
        {
            _arrayCalc = arrayCalc;
        }

        // GET api/arraycalc/reverse
        [HttpGet("reverse")]
        public async Task<ActionResult<int[]>> Get(int[] productids)
        {
            if (productids.Length <= 0)
                return BadRequest("Invalid Parameter");

            int []res = await _arrayCalc.Reverse(productids);

            return Ok(res);
        }

        // GET api/arraycalc/deletepart
        [HttpGet("deletepart")]
        public async Task<ActionResult<int[]>> Get(int position, int[] productids)
        {
            if (position >= productids.Length || position < 0)
                return BadRequest("Invalid Parameters");

            int[] result = await _arrayCalc.Delete(position, productids);
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

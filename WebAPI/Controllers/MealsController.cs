using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        IMealService _mealService;
        public MealsController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [Authorize]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _mealService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addmeal")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] string meal)
        {
            var mealObject = JsonConvert.DeserializeObject<Meal>(meal);

            var result = _mealService.Add(file, mealObject);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }

        [HttpPost("updatemeal")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] string meal)
        {
            var mealObject = JsonConvert.DeserializeObject<Meal>(meal);

            var result = _mealService.Update(file, mealObject);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletemeal")]
        public IActionResult Delete(Meal meal)
        {
            var result = _mealService.Delete(meal);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

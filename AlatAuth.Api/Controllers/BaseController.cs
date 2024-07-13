using AlatAuth.Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlatAuth.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public CreatedResult Created(object value)
        {
            return base.Created("", value);
        }
        protected new IActionResult Response(ApiResponse response)
        {
            if (response.StatusCode == "00")
                return Ok(response);
            if (response.StatusCode == "400")
                return BadRequest(response);
            if (response.StatusCode == "401")
                return Unauthorized(response);
            if (response.StatusCode == "404")
                return NotFound(response);
            if (response.StatusCode == "409")
                return Conflict(response);
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}

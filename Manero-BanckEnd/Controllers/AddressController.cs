using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Manero_BanckEnd.Controllers
{
    [Route("api/Address")]
    [ApiController]
    [UseApiKey]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            
            _addressService = addressService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProfile(AddressCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var streetName = User.FindFirstValue(ClaimTypes.StreetAddress);
                var result = await _addressService.CreateAddress(userEmail, streetName, request);

                return result.Status switch
                {
                    ResponseStatusCode.CREATED => Created("", result),
                    ResponseStatusCode.ERROR => Conflict(result.Message),
                    ResponseStatusCode.NOTFOUND => NotFound(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAddress()
        {
            try
            {

                var streetName = User.FindFirstValue(ClaimTypes.StreetAddress);
                var result = await _addressService.GetAddress(streetName);

                return result.Status switch
                {
                    ResponseStatusCode.OK => Ok(result.Result),
                    ResponseStatusCode.NOTFOUND => NotFound(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfile(AddressUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                var result = await _addressService.UpdateAddress(userEmail, request);

                return result.Status switch
                {
                    ResponseStatusCode.OK => Ok(result),
                    ResponseStatusCode.ERROR => Conflict(result.Message),
                    ResponseStatusCode.NOTFOUND => NotFound(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }
    }
}

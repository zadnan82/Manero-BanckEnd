using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Manero_BanckEnd.Controllers
{
    [Route("api/Address")]
    [ApiController]
    [Authorize(Policy = "JwtPolicy")]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        private readonly DataContext _dbContext;

        public AddressController(AddressService addressService, DataContext dbContext)
        {
            
            _addressService = addressService;
            _dbContext = dbContext;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAddress(AddressCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var result = await _addressService.CreateNewAddress(userEmail, request);

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
        public async Task<IActionResult> GetAddress(string streetName)
        {
            try
            {
                
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
        public async Task<IActionResult> UpdateAddress(string title, AddressUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                
                var currentAddressTitle = await _dbContext.AddressTypes.FirstOrDefaultAsync(a => a.Title == title);
                var currentAddress =
                    await _dbContext.Addresses.FirstOrDefaultAsync(s => s.Id == currentAddressTitle.AddressId);

                var result = await _addressService.UpdateAddress(currentAddress.StreetName, currentAddressTitle.Title, request);

                return result.Status switch
                {
                    ResponseStatusCode.OK => Ok(result.Result),
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

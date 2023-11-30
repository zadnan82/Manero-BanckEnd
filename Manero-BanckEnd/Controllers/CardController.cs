using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Schemas;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Manero_BanckEnd.Controllers;

[Route("api/Card")]
[ApiController]
[Authorize]
[UseApiKey]
public class CardController : ControllerBase
{
    private readonly CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost("CREATE")]
    public async Task<IActionResult> AddCreditCard([FromBody] CardCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credit card information");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Unable to determine the user associated with this request");
            }


            var result = await _cardService.CreateCardAsync(request, userEmail);

            return result.Status switch
            {
                ResponseStatusCode.CREATED => Created("Card has been Created!", result.Result),
                ResponseStatusCode.EXIST => Conflict(result.Message),
                _ => Problem(result.Message),
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Problem();
        }
    }

    [HttpGet("GetAllUserCards")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credit card information");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Unable to determine the user associated with this request");
            }

            var cards = await _cardService.GetAllAsync(userEmail);

            return Ok(cards);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Problem();
        }
    }

    [HttpPut("EditCard")]
    public async Task<IActionResult> PutAsync(CardUpdateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credit card information");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Unable to determine the user associated with this request");
            }


            var result = await _cardService.PutAsync(request, userEmail);

            return result.Status switch
            {
                ResponseStatusCode.OK => Created("Card has been Changed!", result.Result),
                ResponseStatusCode.EXIST => Conflict(result.Message),
                _ => Problem(result.Message),
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Problem();
        }

    }

    [HttpDelete("DELETE")]
    public async Task<IActionResult> DeleteCardAsync(CardDeleteRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credit card information");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("Unable to determine the user associated with this request");
            }


            var result = await _cardService.DeleteAsync(request, userEmail);

            return result.Status switch
            {
                ResponseStatusCode.OK => Created("Card has been removed", result.Result),
                ResponseStatusCode.EXIST => Conflict(result.Message),
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

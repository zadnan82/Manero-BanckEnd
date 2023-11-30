using Manero_BanckEnd.Schemas.PromoCodes;
using Manero_BanckEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_BanckEnd.Controllers;

[Route("api/PromoCode")]
[ApiController]
public class PromoCodeController : ControllerBase
{
    private readonly PromoCodeService _promoCodeService;

    public PromoCodeController(PromoCodeService promoCodeService)
    {
        _promoCodeService = promoCodeService;
    }


    [HttpGet("GETALL")]
    public async Task<IActionResult> GetAllAsync()
    {
        var list = await _promoCodeService.GetAllAsync();
        return StatusCode(200, list);
    }

    [HttpDelete("DELETE")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var promoCode = await _promoCodeService.DeleteAsync(id);
        return StatusCode(200, promoCode);
    }

    [HttpPost("CREATE")]
    public async Task<IActionResult> CreateAsync(PromoCodeSchema promoCodeSchema)
    {
        var promoCode = await _promoCodeService.CreateAsync(promoCodeSchema);
        return StatusCode(201, promoCode);
    }
}

using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController() : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAssetsAsync([FromServices] ICoinloreService coinloreService)
    {
        var assets = await coinloreService.GetAllCryptoAssetsAsync();
        return Ok(assets);
    }
}
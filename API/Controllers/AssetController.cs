using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController(AssetRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> AddAsset()
    {
        var asset = new AssetEntity
        {
            Name = "Test Asset",
        };
        
        await repository.AddAsync(asset);
        return Ok();
    }
}
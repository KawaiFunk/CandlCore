﻿using Application.Handlers.AssetHandlers;
using Application.Interfaces.Mediator;
using Application.Interfaces.Services;
using Application.Models.Assets;
using Domain.Common.PagedList;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AssetController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IPagedList<AssetModel>))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllAsync([FromQuery] PagedListFilter filter)
    {
        try
        {
            var request = new GetAssetsRequest(filter);
            var response = await _mediator.SendAsync(request);
        
            if (response == null)
            {
                return BadRequest("Failed to retrieve assets.");
            }
        
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("{id:length(24)}")]
    [ProducesResponseType(200, Type = typeof(AssetModel))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var request = new GetAssetByIdRequest(id);
            var response = await _mediator.SendAsync(request);
        
            if (response == null)
            {
                return NotFound($"Asset with ID {id} not found.");
            }
        
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}
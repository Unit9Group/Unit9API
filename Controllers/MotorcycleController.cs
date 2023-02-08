using AutoApi.Models;
using AutoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotorcycleController : ControllerBase
{
    private readonly MotorcycleService _motorcycleService;

    public MotorcycleController(MotorcycleService motorcycleService) =>
        _motorcycleService = motorcycleService;
    [HttpGet]

    public async Task<List<Motorcycle>> Get() =>
        await _motorcycleService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Motorcycle>> Get(string id)
    {
        var motorcycle = await _motorcycleService.GetAsync(id);

        if (motorcycle is null)
        {
            return NotFound();
        }

        return motorcycle;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Motorcycle newMotorcycle)
    {
        await _motorcycleService.CreateAsync(newMotorcycle);

        return CreatedAtAction(nameof(Get), new { id = newMotorcycle.Id }, newMotorcycle);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Motorcycle updatedMotorcycle)
    {
        var motorcycle = await _motorcycleService.GetAsync(id);

        if (motorcycle is null)
        {
            return NotFound();
        }

        updatedMotorcycle.Id = motorcycle.Id;

        await _motorcycleService.UpdateAsync(id, updatedMotorcycle);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var motorcycle = await _motorcycleService.GetAsync(id);

        if (motorcycle is null)
        {
            return NotFound();
        }

        await _motorcycleService.RemoveAsync(id);

        return NoContent();
    }
}
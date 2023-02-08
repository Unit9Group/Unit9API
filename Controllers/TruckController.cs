using AutoApi.Models;
using AutoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TruckController : ControllerBase
{
    private readonly TruckService _autoService;

    public TruckController(TruckService autoService) =>
        _autoService = autoService;
    [HttpGet]

    public async Task<List<Truck>> Get() =>
        await _autoService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Truck>> Get(string id)
    {
        var truck = await _autoService.GetAsync(id);

        if (truck is null)
        {
            return NotFound();
        }

        return truck;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Truck newTruck)
    {
        await _autoService.CreateAsync(newTruck);

        return CreatedAtAction(nameof(Get), new { id = newTruck.Id }, newTruck);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Truck updatedTruck)
    {
        var truck = await _autoService.GetAsync(id);

        if (truck is null)
        {
            return NotFound();
        }

        updatedTruck.Id = truck.Id;

        await _autoService.UpdateAsync(id, updatedTruck);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var truck = await _autoService.GetAsync(id);

        if (truck is null)
        {
            return NotFound();
        }

        await _autoService.RemoveAsync(id);

        return NoContent();
    }
}
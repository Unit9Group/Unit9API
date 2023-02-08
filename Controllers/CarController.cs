using AutoApi.Models;
using AutoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService) =>
        _carService = carService;
    [HttpGet]

    public async Task<List<Car>> Get() =>
        await _carService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Car>> Get(string id)
    {
        var car = await _carService.GetAsync(id);

        if (car is null)
        {
            return NotFound();
        }

        return car;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Car newCar)
    {
        await _carService.CreateAsync(newCar);

        return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Car updatedCar)
    {
        var car = await _carService.GetAsync(id);

        if (car is null)
        {
            return NotFound();
        }

        updatedCar.Id = car.Id;

        await _carService.UpdateAsync(id, updatedCar);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var car = await _carService.GetAsync(id);

        if (car is null)
        {
            return NotFound();
        }

        await _carService.RemoveAsync(id);

        return NoContent();
    }
}
using Common;
using dyno_api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dyno_api.Controllers;

[Authorize]
[Route($"api/{Routes.Measurements}")]
[ApiController]
public class MeasurementController : ControllerBase
{
    private readonly IRepository<MeasurementEntity> _measurementRepository;

    public MeasurementController(IRepository<MeasurementEntity> measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }

    // GET: api/<MeasurementController>
    [HttpGet]
    public async Task<ActionResult<List<MeasurementModel>>> Get()
    {
        var measurements = (await _measurementRepository.GetAll())
            .OrderByDescending(x => x.DateTimeOffset);
        return Ok(measurements.Select(MeasurementConverters.Convert).ToList());
    }

    // GET api/<MeasurementController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MeasurementModel>> Get(Guid id)
    {
        var measurement = await _measurementRepository.Get(id);
        return Ok(MeasurementConverters.Convert(measurement));
    }

    // POST api/<MeasurementController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MeasurementModel measurementModel)
    {
        // Some validation here perhaps. 
        var sub = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(sub, out var userId))
            return Problem();
        // Här blir det FK-fel. troligtvis för att användaren inte finns eller nåt sånt. Men konstigt att det då gick att patcha db. Aja. Hej da!! 
        var entity = MeasurementConverters.ConvertWithUserId(measurementModel, userId);
        var result = await _measurementRepository.Create(entity);

        if(!result)
            return BadRequest(result);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _measurementRepository.Delete(id);

        if (result)
            return Ok();

        return Problem();
    }
}

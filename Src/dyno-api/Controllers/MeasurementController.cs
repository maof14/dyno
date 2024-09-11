using Common;
using dyno_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dyno_api.Controllers;

[Route($"api/{Routes.Measurements}")]
[ApiController]
public class MeasurementController : ControllerBase
{
    private readonly IRepository<Measurement> _measurementRepository;

    public MeasurementController(IRepository<Measurement> measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }

    // GET: api/<MeasurementController>
    [HttpGet]
    public ActionResult<List<MeasurementModel>> Get()
    {
        // Add converters?
        var measurements = _measurementRepository.GetAll();
        return Ok(measurements.Select(MeasurementConverters.Convert).ToList());
    }

    // GET api/<MeasurementController>/5
    [HttpGet("{id}")]
    public ActionResult<MeasurementModel> Get(Guid id)
    {
        var measurement = _measurementRepository.Get(id);
        return Ok(MeasurementConverters.Convert(measurement));
    }

    // POST api/<MeasurementController>
    [HttpPost]
    public ActionResult Post([FromBody] MeasurementModel measurementModel)
    {
        // Some validation here perhaps. 

        var entity = MeasurementConverters.Convert(measurementModel);
        var result = _measurementRepository.Create(entity);

        return Ok();
    }

    // PUT api/<MeasurementController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<MeasurementController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

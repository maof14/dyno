using Common;
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
    public List<MeasurementModel> Get()
    {
        // Add converters?
        var measurements = _measurementRepository.GetAll();
        return measurements.Select(x => new MeasurementModel(id: x.Id, dateTime: x.DateTime, measurementResults: x.MeasurementResults.Select(y => new MeasurementResultModel(id: y.Id, datapoint: y.DataPoint, dateTimeRecorded: y.DateTimeRecorded)).ToList())).ToList();
    }

    // GET api/<MeasurementController>/5
    [HttpGet("{id}")]
    public MeasurementModel Get(Guid id)
    {
        var measurement = _measurementRepository.Get(id);

        return new MeasurementModel(id: measurement.Id, dateTime: measurement.DateTime, measurementResults: measurement.MeasurementResults.Select(x => new MeasurementResultModel(id: x.Id, datapoint: x.DataPoint, dateTimeRecorded: x.DateTimeRecorded)).ToList());
    }

    // POST api/<MeasurementController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
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

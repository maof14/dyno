
namespace dyno_server.Contract
{
    public class MonitorResult
    {
        public List<Result> Results { get; set; } = new List<Result>();

        public Guid Guid { get; }
        
        public MonitorResult(Guid guid)
        {
            Guid = guid;
        }

        public void AddDataPoint(Result result)
        {
            Results.Add(result);
        }
    }

    public class Result
    {
        public int DataPoint { get; }
        public DateTimeOffset DateTimeRecorded { get; }

        public Result(int dataPoint, DateTimeOffset dateTimeRecorded)
        {
            DataPoint = dataPoint;
            DateTimeRecorded = dateTimeRecorded;
        }
    }
}
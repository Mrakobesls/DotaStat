using Newtonsoft.Json;

namespace WebApplication.ViewModel
{
    public class DataPoint
    {
        [JsonProperty("label")]
        public object X;
        [JsonProperty("y")]
        public object Y;
        public DataPoint(object x, object y)
        {
            X = x;
            Y = y;
        }
    }
}

using Newtonsoft.Json;

namespace DotaStat.Business.Interfaces.Models
{
    public class HeroesRequest
    {
        [JsonProperty("result")]
        public HeroesResult Result { get; set; }
    }

    public class HeroesResult
    {
        [JsonProperty("heroes")]
        public HeroRequest[] Heroes { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class HeroRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }

    }

}

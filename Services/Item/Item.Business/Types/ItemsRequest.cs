using Newtonsoft.Json;

namespace Item.Business.Types;

public class ItemsRequest
{
    [JsonProperty("result")]
    public ItemsResult Result { get; set; }
}

public class ItemsResult
{
    [JsonProperty("items")]
    public HeroResult[] Items { get; set; }
    [JsonProperty("status")]
    public int Status { get; set; }
    [JsonProperty("count")]
    public int Count { get; set; }
}

public class HeroResult
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("localized_name")]
    public string LocalizedName { get; set; }
}

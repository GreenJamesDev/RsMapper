using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsMapper
{
    /// <summary>
    /// Represents a redstone component.
    /// </summary>
    public class RsComponent
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("image")]
        public string image { get; set; }
        [JsonProperty("info")]
        public string info { get; set; }
        [JsonProperty("acceptswire")]
        public string acceptswire { get; set; }
    }

    /// <summary>
    /// Represents the root level array of the JSON file.
    /// </summary>
    public class RootObject
    {
        [JsonProperty("RsComponents")]
        public List<RsComponent> RsComponents { get; set; }
    }
}

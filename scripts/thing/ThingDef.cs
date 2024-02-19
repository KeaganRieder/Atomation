using Newtonsoft.Json;
namespace Atomation.Thing
{
    /// <summary>
    /// base config file used for all things in game
    /// </summary>
    public abstract class ThingDef : IThing
    {
        //maybe make inherit from resource/make this a custom resources?
        [JsonProperty("name",Order = 1)]
        public string Name { get; set; }
        [JsonProperty("description", Order = 2)]
        public string Description { get; set; }

    }

   
}
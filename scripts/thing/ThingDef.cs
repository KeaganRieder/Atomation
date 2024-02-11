using Newtonsoft.Json;
namespace Atomation.Thing
{
    /// <summary>
    /// base config file used for all things in game
    /// </summary>
    public abstract class ThingDef : IThingDef
    {
        //maybe make inherit from resource/make this a custom resources?
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

    }

    public interface IThingDef
    {
        public string Name { get; set; }
    }
}
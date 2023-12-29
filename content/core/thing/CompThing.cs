using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// base defintion for all complex things, used to define
/// values which all complex thing share
/// </summary>
public partial class CompThing : IThing
{
    [JsonProperty("name")]
    public string Name{get; set;}
    [JsonProperty("label")]
    public string Label{get; set;}
    [JsonProperty("description")]
    public string Description{get; set;}
    [JsonProperty]
    protected Dictionary<string,StatBase> stats;
    [JsonProperty]
    protected Graphic graphic;

    public CompThing(string name, string description) {Description = description; Name = name; Label = name;}
    public CompThing(string name, string description, Dictionary<string,StatBase> stats, Graphic graphic)
        : this(name,description){

        this.stats = stats;
        this.graphic = graphic;
    }   

}

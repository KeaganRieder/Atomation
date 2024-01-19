using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// base defintion for all complex things, used to define
/// values which all complex thing share
/// </summary>
/// //todo get rid of node2d
public partial class CompThing : Node2D, IThing
{
    // [JsonProperty("name")]
    // public string Name{get => base.Name; set{ base.Name;}
    [JsonProperty("label")]
    public string Label{get; set;}
    [JsonProperty("description")]
    public string Description{get; set;}
    [JsonProperty]
    protected Dictionary<string,StatBaseOld> stats;
    [JsonProperty]
    protected Graphic graphic;
 
    public CompThing(string name, string description) {Description = description; Name = name; Label = name;}
    public CompThing(string name, string description, Dictionary<string,StatBaseOld> stats, Graphic graphic)
        : this(name,description){

        this.stats = stats;
        this.graphic = graphic;
    }   

}

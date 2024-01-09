using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// the games player's character
/// </summary>
public partial class Player
{
    [JsonProperty]
    protected Dictionary<string,StatBase> stats;
    [JsonProperty]
    protected Graphic graphic;
}
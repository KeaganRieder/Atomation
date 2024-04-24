namespace Atomation.Things;

using Godot;
using Atomation.Map;
using Atomation.Resources;
using Newtonsoft.Json;

/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a complex things
/// </summary>
public abstract class CompThingDetails : Thing
{
	[JsonProperty("Graphic Data", Order = 1)]
	public GraphicData GraphicData { get; set; }

	[JsonProperty("Stat Sheet", Order = 2)]
	public StatSheet StatSheet{get;set;}

	protected CompThingDetails(){}
	protected CompThingDetails(string name, string description,StatSheet statSheet,GraphicData graphicData)
	 : base(name,description){
		GraphicData = graphicData;
		if (graphicData.GraphicSize == Vector2I.Zero)
        {
            graphicData.GraphicSize = new Vector2I(WorldMap.CELL_SIZE,WorldMap.CELL_SIZE);
        }
		StatSheet = statSheet;

	}

}

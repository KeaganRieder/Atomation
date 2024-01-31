using System;
using System.Linq;
using Godot;

/// <summary>
/// define sthe games World map, storing and allowing of manuiplaution/accses to vth evarious
/// varibles that realet to the map and it's various aspects
/// </summary>
public partial class WorldMap : Node2D
{
	public const float CELL_SIZE = 16; 
	public int Test{get; set;} = 100;
	public int Width{get; set;}
	public int Height{get; set;}
	public Vector2 MapSize{get => new Vector2(Width,Height);}

	//map componets
	private WorldGenerator mapGenerator;
	private ChunkHandler chunkHandler;
	private Node2D PlayerNode;
   
	public WorldMap(){
		Width = 100;
		Height = 100;
		//note zoom level may need a be something like 1000
		GenConfigs genConfig = new GenConfigs(){
			worldBounds = new Vector2I(Width,Height),
			seaLevel = -0.1f,
			mounatinSize = 0.2f,
			elevationMapConfigs = new NoiseMapConfig(){
				seed = 0,
				octaves = 6,
				zoom = 75,
				frequency = 2,
				lacunarity = 2,
				persistence = 0.6f,
			},
			moistureMapConfigs = new NoiseMapConfig(){
				seed = 0,
				octaves = 6,
				zoom = 75,
				frequency = 2,
				lacunarity = 2,
				persistence = 0.6f,
			},
			heatMapConfigs = new NoiseMapConfig(){
				seed = 0,
				octaves = 6,
				zoom = 75,
				frequency = 2,
				lacunarity = 2,
				persistence = 0.6f,
			}
		};
		
		mapGenerator = new WorldGenerator(genConfig);

		chunkHandler = new ChunkHandler(this);	
		PlayerNode = new Node2D(){Name = "player"};
		PlayerNode.AddChild(new ColorRect(){Color = new Color(100,100,100), Size = new Vector2(16,16)});	
		AddChild(PlayerNode);
	}

	public override void _Ready(){
		base._Ready();
		GD.Print("test gen");
		chunkHandler.UpdateRenderedChunks(PlayerNode.Position,mapGenerator);
		// mapGenerator.GenerateMap(this);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		chunkHandler.UpdateRenderedChunks(PlayerNode.Position,mapGenerator);
		
	}


}

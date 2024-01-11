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
	private MapGenerator mapGenerator;
	private ChunkHandler chunkHandler;
	private Node2D PlayerNode;
   
	public WorldMap(){
		Width = 64;
		Height = 64;

		//todo
		mapGenerator = new MapGenerator(Width,Height)
		{
			Seed = 0,
			Octaves = 6,
			ZoomLevel = 75,
			Frequency = 2,
			Lacunarity = 2,
			Persistence = .6f,
		};

		chunkHandler = new ChunkHandler(mapGenerator, this);	
		PlayerNode = new Node2D(){Name = "player"};
		PlayerNode.AddChild(new ColorRect(){Color = new Color(100,100,100), Size = new Vector2(16,16)});	
		AddChild(PlayerNode);
	}

	public override void _Ready(){
		base._Ready();
		GD.Print("test gen");
		chunkHandler.UpdateChunks(PlayerNode.Position,mapGenerator);
		// mapGenerator.GenerateMap(this);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		chunkHandler.UpdateChunks(PlayerNode.Position,mapGenerator);
		
	}


}

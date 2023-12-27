using System;
using Godot;

/// <summary>
/// holds data for the map
/// </summary>
public struct Data{
	public const int ChunkSize = 32;
	public const float MAX_VIEW_DIST  = 64;

	//map size
	public int Width{get; set;}
	public int Height{get; set;}

	//generation values
	public int seed{get; set;}

	//add these float frequency,float lacunarity, float fractalGain
}

/// <summary>
/// the games map, stores data about it, as well as handles/allows
/// for differnt elements to be interacted with
/// </summary>
public partial class Map : Node{

	public static Data mapData;
	private MapGenerator mapGenerator;

	public Map(){
		mapData.Width = 100;
		mapData.Height = 100;
		GD.Print($"Width:{mapData.Width} height:{mapData.Height}");
		mapGenerator = new MapGenerator();
	}


	public override void _Ready()
	{
		GD.Print("test gen");
		mapGenerator.GenerateChunk();

		base._Ready();

		
	}
}

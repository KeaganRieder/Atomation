using System;
using Godot;

/// <summary>
/// holds data for the map
/// </summary>
public struct MapData{
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

	public static MapData mapData;
	private MapGenerator mapGenerator;

	public Map(){
		mapData.Width = 512;
		mapData.Height = 512;
		GD.Print($"Width:{mapData.Width} height:{mapData.Height}");
		mapGenerator = new MapGenerator();
	}


	public override void _Ready()
	{
		base._Ready();
		GD.Print("test gen");
		// Sprite2D test = new Sprite2D();
		// AddChild(test);
		// NoiseTexture2D texture2D = new NoiseTexture2D(){
		// 	Width = mapData.Width,
		// 	Height = mapData.Height
		// };
	
		
		// mapGenerator.GenerateChunk(texture2D);
		// test.Texture = texture2D;	
	}
}

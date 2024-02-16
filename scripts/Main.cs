using Atomation.Map;
using Atomation.Resources;
using Godot;

/// <summary>
/// Main class which handles manning the game through different scene
/// </summary>
public partial class Main : Node
{
	private WorldMap map;
	private FileManger fileManger;

	public Main()
	{
		fileManger = new FileManger();
	}


	/// <summary>
	/// runs upon node creation
	/// </summary>
	public override void _Ready()
	{	
		GameStartUp();
		map = new WorldMap();
		AddChild(map);
		base._Ready();	
	}

	/// <summary>
	/// function which runs all process that are needed during game 
	/// start up
	/// Note this is still needing work
	/// </summary>
	public void GameStartUp()
	{
		fileManger.LoadFiles();
	}

	//TODO add more function and management things
}

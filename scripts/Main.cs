using Atomation.Map;
using Atomation.Resources;
using Atomation.System;
using Godot;

/// <summary>
/// Main class which handles manning the game through different scene
/// </summary>
public partial class Main : Node
{
	private WorldMap map;
	private FileManger resourceManger;
	private KeyBindings keyBindings;

	public Main()
	{
		resourceManger = new FileManger();
		keyBindings = new KeyBindings();
	}


	/// <summary>
	/// runs upon node creation
	/// </summary>
	public override void _Ready()
	{			
		// keyBindings.FormatFile("default_bindings");
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
		resourceManger.LoadFiles();
		keyBindings.LoadBindings("default_bindings");
	}

	//TODO add more function and management things
}

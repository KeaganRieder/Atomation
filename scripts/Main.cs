using Atomation.Map;
using Atomation.Resources;
using Atomation;
using Godot;
using Atomation.Player;
using Atomation.Thing;

/// <summary>
/// Main class which handles manning the game through different scene
/// </summary>
public partial class Main : Node
{
	public WorldMap Map { get; private set; }
	public FileManger ResourceManger { get; private set; }
	public KeyBindings KeyBindings { get; private set; }
	public PlayerChar Player { get; private set; }

	public Main()
	{
		ResourceManger = new FileManger();
		KeyBindings = new KeyBindings();
	}

	/// <summary>
	/// runs upon node creation
	/// </summary>
	public override void _Ready()
	{
		// keyBindings.FormatFile("default_bindings");
		GameStartUp();

		PlayerChar Player = new PlayerChar();
		Map = new WorldMap(Player);
		AddChild(Map);

		AddChild(Player);
		base._Ready();
	}

	/// <summary>
	/// function which runs all process that are needed during game 
	/// start up
	/// Note this is still needing work
	/// </summary>
	public void GameStartUp()
	{
		ResourceManger.LoadFiles();
		KeyBindings.LoadBindings("default_bindings");
	}

	//TODO add more function and management things
}

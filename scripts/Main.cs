using Atomation;
using Atomation.Map;
using Atomation.Resources;
using Atomation.Player;
using Godot;
/// <summary>
/// Main class which handles manning the game through different scene
/// </summary>
public partial class Main : Node
{
	public WorldMap Map { get; private set; }
	public FileManger ResourceManger { get; private set; }
	public PlayerChar Player { get; private set; }
	public Controller controller { get; private set; }

	public Main()
	{
		ResourceManger = new FileManger();
	}

	/// <summary>
	/// runs upon node creation
	/// </summary>
	public override void _Ready()
	{
		ResourceManger.LoadFiles();

		PlayerChar Player = new PlayerChar();
		Map = new WorldMap(Player);

		controller = new Controller();
		controller.Map = Map;
		controller.PlayerBody = Player;

		AddChild(Map);
		AddChild(Player);
		AddChild(controller);

		base._Ready();
	}


}

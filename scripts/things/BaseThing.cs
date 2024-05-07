namespace Atomation.Things;

using Atomation.Resources;
using Atomation.Map;
using Godot;

public interface IThing
{
	public Node2D Node { get; }

	public abstract void SetPosition(Coordinate coordinate);
	public abstract void SetPosition(Vector2 Position);

}

/// <summary>
/// base of all complex things inside the game. these are things which 
/// generally appear in the world or have more complex functionality
/// </summary>
public abstract partial class BaseThing : Node2D, IThing
{
	protected Coordinate coordinate;

	public string DefName { get; protected set; }
	public string Description { get; set; }
	public StatSheet StatSheet { get; protected set; }
	public Node2D Node { get => this; }
	public StaticGraphic Graphic { get; set; }

	public void DestroyNode()
	{
		if (IsInstanceValid(Node))
		{
			Node.QueueFree();
		}
	}

	public void SetPosition(Coordinate coordinate)
	{
		this.coordinate = coordinate;
		Position = coordinate.GetWorldPosition();
	}
	public void SetPosition(Vector2 Position)
	{
		Coordinate cord = new Coordinate(Position);
		SetPosition(cord);
	}

	public Coordinate GetCoordinate(){
		return coordinate;
	}


}
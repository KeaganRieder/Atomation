namespace Atomation.Things;

using Atomation.Resources;
using Atomation.Map;
using Godot;

public interface IThing
{
	public Node2D Node { get; }
	public Coordinate Coordinate { get; }
}

/// <summary>
/// base of all complex things inside the game. these are things which 
/// generally appear in the world or have more complex functionality
/// </summary>
public abstract partial class Thing : Node2D, IThing
{
	protected Coordinate coordinate;
	public string DefName { get; protected set; }
	public string Description { get; set; }
	public Coordinate Coordinate { get => coordinate; protected set { coordinate = value; } }
	public StatSheet StatSheet { get; protected set; }

	public Node2D Node { get => this; }

	public StaticGraphic Graphic { get; set; }

	public virtual void Heal(float amount) { }
	public virtual void Damage(float amount) { }

	public virtual void DestroyNode()
	{
		if (IsInstanceValid(Node))
		{
			Node.QueueFree();
		}
	}
}


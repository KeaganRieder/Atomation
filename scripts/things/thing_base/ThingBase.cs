namespace Atomation.Things;

using Resources;
using Map;
using Godot;
using Newtonsoft.Json;

public interface IThing
{
    public abstract void SetPosition(Coordinate coordinate);
    public abstract void SetPosition(Vector2 Position);

    public abstract Node2D GetNode();
}

/// <summary>
/// base class for all things that appear in the
/// game world
/// </summary>
public abstract class ThingBase : IThing
{
    [JsonProperty(Order = -1)]
    protected string defName;

    protected string description;

    [JsonProperty(Order = -1)]
    protected Coordinate cords;
    [JsonProperty(Order = -1)]
    protected StatSheet statSheet;
    protected Node2D node;

    protected ThingBase() { }
    ~ThingBase()
    {
        DestroyNode();
    }

    public virtual void SetName(string name)
    {
        defName = name;
    }
    public virtual void SetDescription(string desc)
    {
        description = desc;
    }

    public virtual void SetPosition(Coordinate cord)
    {
        cords = cord;
        if (node != null)
        {
            node.Position = cords.GetWorldPosition();

        }
    }
    public virtual void SetPosition(Vector2 cord)
    {
        if (cords != null)
        {
            cords.SetPosition(cord);
        }
        else
        {
            cords = new Coordinate(cord);
        }
        node.Position = cords.GetWorldPosition();
    }

    public virtual string GetName()
    {
        return defName;
    }
    public virtual string GetDescription()
    {
        return description;
    }
    public virtual Coordinate GetCoordinate()
    {
        if (cords != null)
        {
            return cords;
        }
        else
        {
            return null;
        }
    }
    public virtual StatSheet GetStatSheet()
    {
        if (statSheet != null)
        {
            return statSheet;
        }
        else
        {
            return null;
        }
    }

    public virtual Node2D GetNode()
    {
        return node;
    }
    public virtual void DestroyNode()
    {
        if (GodotObject.IsInstanceValid(node))
        {
            foreach (var child in node.GetChildren())
            {
                child.QueueFree();
            }
            node.QueueFree();
        }
    }
}
namespace Atomation.Player;

using Atomation.GameMap;
using Atomation.Systems;
using Atomation.Things;
using Atomation.Ui;
using Godot;

/// <summary>
/// receives input from the player. it then process that input and decide
/// what action to take/perform. sometime this will pass direction to other class
/// on how to handle the input their depending on teh complexity of the action
/// </summary>
public partial class PlayerController : Node2D
{
    private PlayerCharacter playerTarget;
    private CustomCamera cameraTarget;
    private Map mapTarget;

    // private Inventory inventoryTarget; todo

    //do events
    //  [Signal]
    public delegate void WorldInteractionEventHandler(); //https://learn.microsoft.com/en-us/dotnet/standard/events/
    // public delegate

    public PlayerController(PlayerCharacter player)
    {
        Name = "playerController";
        playerTarget = player;
        player.AddChild(this);

        cameraTarget = playerTarget.Camera;
        new PauseMenu(player.Camera);
    }

    /// <summary>
    /// sets the controls game map target
    /// </summary>
    public void SetMapTarget(Map mapTarget)
    {
        this.mapTarget = mapTarget;
    }

    public override void _Input(InputEvent input)
    {
        base._Input(input);
        HandleGeneralInput();
        HandleMovementInput();
        HandleCameraInputs();

        HandleInteractionInput(input);
        HandleInventoryInputs(input);
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        //todo
        base._UnhandledInput(inputEvent);
    }

    /// <summary>
    /// gets the mouse position aligned to the map cord,
    /// using GlobalToMap in cord utility
    /// </summary>
    public Vector2 GetMouseMapPosition()
    {
        Vector2 globalPosition = GetGlobalMousePosition() + new Vector2(Map.CELL_SIZE / 2, Map.CELL_SIZE / 2);

        return globalPosition.GlobalToMap();
    }

    /// <summary>
    /// gets the mouse position aligned to the chunk cord,
    /// using GlobalToMap and MapToChunk in cord utility
    /// </summary>
    public Vector2 GetMouseChunkPosition()
    {
        Vector2 chunkPosition = GetMouseMapPosition().MapToChunk();
        return chunkPosition;
    }

    /// <summary>
    /// handles input from bindings which relate to
    /// actions that don't really belong to a category
    /// </summary>
    private void HandleGeneralInput()
    {

    }

    /// <summary>
    /// handles input from bindings which relate to
    /// character movement
    /// </summary>
    private void HandleMovementInput()
    {
        Vector2 inputDirection = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
        playerTarget.UpdateVelocity(inputDirection);
    }

    /// <summary>
    /// handles input from bindings which relate to
    /// camera movement
    /// </summary>
    private void HandleCameraInputs()
    {
        if (Input.IsActionPressed("ZoomIn"))
        {
            cameraTarget.ZoomIn();
        }
        if (Input.IsActionPressed("ZoomOut"))
        {
            cameraTarget.ZoomOut();
        }
    }

    /// <summary>
    /// handles input from bindings which relate to
    /// inventory actions
    /// </summary>
    private void HandleInventoryInputs(InputEvent input)
    {
        if (input.IsActionPressed("Inventory"))
        {
            playerTarget.Inventory.ToggleUI();
        }
        //todo
    }

    /// <summary>
    /// handles input from bindings which relate to
    /// interaction of things
    /// </summary>
    private void HandleInteractionInput(InputEvent input)
    {
        //todo make hovering interaction, to get some information form tiles

        if (input is InputEventMouseButton)
        {
            if (input.IsActionPressed("Interact"))
            {
                Vector2 mouseChunkPosition = GetMouseChunkPosition();
                Vector2 mouseTilePosition = (GetMouseMapPosition() - mouseChunkPosition * Chunk.CHUNK_SIZE).Abs();

                //need to clean up somehow
                Plant plant = mapTarget.ChunkHandler.GetChunk(mouseChunkPosition).GetPlant(mouseTilePosition);

                Structure structure = mapTarget.ChunkHandler.GetChunk(mouseChunkPosition).GetStructure(mouseTilePosition);
                Item item = mapTarget.ChunkHandler.GetChunk(mouseChunkPosition).GetItem(mouseTilePosition);
                if (structure != null)
                {
                    structure.Damage(playerTarget.StatSheet);
                }
                if (plant != null)
                {
                    plant.Damage(playerTarget.StatSheet);
                }
                else if (item != null)
                {
                    GD.Print($"{item.ID} has {item.CurrentStackSize} stack together");
                }

            }
        }
    }

}
// namespace Atomation.Controls;

// using Atomation.GameMap;
// using Atomation.Pawns;
// using Atomation.Systems;
// using Atomation.Things;
// using Atomation.Ui;
// using Godot;

// /// <summary>
// /// class which handles the players input, deciding how it should 
// /// be handle
// /// </summary>
// public partial class PlayerController : Node
// {
//     private SaveHandler saveHandler;
//     private Player player;
//     private Map worldMap;
//     private Timer chunkUpdateDelay;

//     private GameBindings controlBindings;


//     //signals

//     public PlayerController(Player player)
//     {
//         Name = "PlayerController";
//         worldMap = Map.Instance;
//         saveHandler = SaveHandler.Instance;
//         this.player = player;

//         controlBindings = new GameBindings();

//         player.AddChild(this);
//     }

//     public override void _Input(InputEvent input)
//     {
//         base._Input(input);

//         if (!PauseMenu.Instance.IsOpen)
//         {
//             HandleMovement();
//             if (input.IsActionPressed("Interact"))
//             {
//                 InteractWithWorld((InputEventMouseButton)input);
//             }
//             HandleWorldDebugControls(input);
//             if (input.IsActionPressed("QuickSave"))
//             {
//                 saveHandler.QuickSave();
//             }
//             if (input.IsActionPressed("QuickLoad"))
//             {
//                 saveHandler.QuickLoad();
//             }
//         }

//         HandleGameUiControls(input);
//     }

//     public Vector2 GetMousePosition(InputEventMouseButton input)
//     {
//         Vector2 mousePos = input.Position - GetViewport().CanvasTransform.Origin;
//         return mousePos;
//     }


//     private void HandleMovement()
//     {
//         Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

//         if (direction != Vector2.Zero)
//         {
//             player.Move(direction);
//             worldMap.UpdateVisibleChunks(player.Position.ToCords());
//         }
//     }

//     private void InteractWithWorld(InputEventMouseButton input)
//     {
//         Coordinate cords = new Coordinate(GetMousePosition(input));

//         if (worldMap.GetStructure(cords) != null)
//         {
//             Structure structure = worldMap.GetStructure(cords);

//             structure.Damage(Player.Instance.GetStatSheet());
//             GD.Print($"{structure.GetName()} {structure.GetCoordinate()} HP: {structure.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue}");
//         }
//         else if (worldMap.GetItem(cords) != null)
//         {
//             Item item = worldMap.GetItem(cords);
//             GD.Print($"{item.GetName()}");
//             item.PickUp(Player.Instance.GetInventory());

//         }
//         else if (worldMap.GetTerrain(cords) != null)
//         {
//             Terrain terrain = worldMap.GetTerrain(cords);
//             GD.Print($"{terrain.GetNode().Name}");
//             if (terrain.GetNode().Visible)
//             {
//                 terrain.GetNode().Visible = false;
//             }
//             else
//             {
//                 terrain.GetNode().Visible = true;
//             }
//         }
//         else
//         {
//             GD.Print("nothing here");
//         }
//     }

//     private void HandleWorldDebugControls(InputEvent input)
//     {
//         if (input.IsActionPressed("NewSeed"))
//         {
//             GD.Print("Randomizing Seed");
//             MapData.GetData().RandomizeSeed();
//         }
//         else if (input.IsActionPressed("GenerateNewMap"))
//         {
//             GD.Print("Generating New Map");
//             WorldGenerator.Instance.GenerateMap();
//         }

//         else if (input.IsActionPressed("VisualizeDefault"))
//         {
//             GD.Print("Default Mode");
//             worldMap.SetVisualizationMode(VisualizationMode.Default);
//         }
//         else if (input.IsActionPressed("VisualizeMoisture"))
//         {
//             GD.Print("Moisture Mode");

//             worldMap.SetVisualizationMode(VisualizationMode.Moisture);
//         }
//         else if (input.IsActionPressed("VisualizeHeat"))
//         {
//             GD.Print("Heat Mode");
//             worldMap.SetVisualizationMode(VisualizationMode.Heat);
//         }
//         else if (input.IsActionPressed("VisualizeHeight"))
//         {
//             GD.Print("Elevation Mode");
//             worldMap.SetVisualizationMode(VisualizationMode.Height);
//         }
//     }

//     public void HandleGameUiControls(InputEvent input)
//     {
//         if (input.IsActionPressed("Inventory") && !PauseMenu.Instance.IsOpen)
//         {
//             if (player.GetInventory().IsOpen)
//             {
//                 player.GetInventory().Close();
//             }
//             else
//             {
//                 player.GetInventory().Open();
//             }
//         }
//         else if (input.IsActionPressed("Pause"))
//         {
//             if (PauseMenu.Instance.IsOpen)
//             {
//                 PauseMenu.Instance.Close();
//             }
//             else
//             {
//                 PauseMenu.Instance.Open();         
//             }
//         }
//     }



// }
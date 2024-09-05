namespace Atomation;

using Godot;

using Atomation.GameMap;
using Atomation.Resources;
using Atomation.Player;
using Atomation.Things;
using Atomation.Ui;
using Atomation.Systems;
using Atomation.Settings;

/// <summary>
/// game manger is the main class used to run Atomation. It handles creating the scene tree, and manges 
/// various thing during the games start up
/// </summary>
public partial class GameManger : Node2D
{
   private static GameManger instance;
   public static GameManger Instance
   {
      get
      {
         if (instance == null)
         {
            instance = new GameManger();
         }
         return instance;
      }
   }

   private Map gameMap;
   private PlayerCharacter player;
   private CustomCamera playerCam;


   private GameManger() { }

   public override void _Ready()
   {
      FormatFiles();

      base._Ready();

      InitializeGame();

      LoadResources();

      StartGame();
   }

   /// <summary>
   /// initializes the game by creating the required nodes/objects and beings file loading process
   /// </summary>
   private void InitializeGame()
   {
      playerCam = new CustomCamera();
      playerCam.UpdateTarget(this);

      gameMap = Map.Instance;
      AddChild(gameMap);

      new PlayerKeybindSettings(); //do more work on this
   }

   /// <summary>
   /// loads all resources required by the game
   /// </summary>
   private void LoadResources()
   {
      GD.Print("Loading Resources");
      ThingDatabase.Instance.LoadDefs();
      GD.Print("Loading Complete\n");
   }

   /// <summary>
   /// formats def files if needed
   /// </summary>
   private void FormatFiles()
   {
      // ItemDefs.FormatResourceItemDefs(); todo reformat
      // StructureDefs.FormatNaturalStructureDefs();
      // TerrainDefs.FormatTerrainDefs();
   }

   /// <summary>
   /// starts the game this is a temporary function tell the main menu is created
   /// </summary>
   public void StartGame()
   {
      gameMap.Generate();

      PauseMenu pauseMenu = new PauseMenu(playerCam); 

      player = PlayerCharacter.Instance;
      player.Camera = playerCam;
      playerCam.UpdateTarget(player);
      AddChild(player);
   }

   /// <summary>
   /// handles saving of the game
   /// </summary>
   public void SaveGame(){
      GD.Print("saving not implemented");
   }

   /// <summary>
   /// handles Loading of the game
   /// </summary>
   public void LoadGame(){
      GD.Print("loading not implemented");
   }

}
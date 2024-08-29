namespace Atomation;

using Atomation.GameMap;
using Atomation.Resources;
using Atomation.Player;
using Atomation.Things;
using Godot;
using Atomation.Ui;
using Atomation.Systems;
using Atomation.Settings;



/// <summary>
/// Main class which handles manning the game through different scene
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
   private PlayerKeybindSettings playerKeybindSettings;

   private CustomCamera mainCam;

   // private PauseMenu pauseMenu;

   private GameManger() { }

   /// <summary>
   /// runs upon node creation
   /// </summary>
   public override void _Ready()
   {
      FormatFiles();

      base._Ready();

      InitializeGame();

      LoadResources();

      StartGame();
   }

   /// <summary>
   /// used to initialize atomation
   /// </summary>
   private void InitializeGame()
   {
      mainCam = new CustomCamera();
      mainCam.UpdateTarget(this);

      gameMap = Map.Instance;
      AddChild(gameMap);

      playerKeybindSettings = new PlayerKeybindSettings(); //do more work on this
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

      player = PlayerCharacter.Instance;
      mainCam.UpdateTarget(player);
      AddChild(player);
   }

}
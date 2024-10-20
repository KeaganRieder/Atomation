namespace Atomation;

using Godot;

using Atomation.GameMap;
using Atomation.Resources;
using Atomation.Player;
using Atomation.Systems;
using Atomation.GameSettings;
using Atomation.Things;
using System.Collections.Generic;
using Atomation.Ui;

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

   private CustomCamera mainMenuCam;
   private MainMenu mainMenu;

   private Map gameMap;
   // private PlayerCharacter player;

   private GameManger()
   {
      mainMenuCam = new CustomCamera(this);

      ThingDefDatabase.Instance.ReadFiles();
      new PlayerKeybindSettings();//read in keybindings

   }

   public override void _Ready()
   {
      SetupGame();
      base._Ready();
   }

   /// <summary>
   /// initializes the game, this is performed during start up
   /// </summary>
   private void SetupGame()
   {
      mainMenu = new MainMenu();
      mainMenuCam.AddChild(mainMenu);

      gameMap = Map.Instance;
      AddChild(gameMap);

      FinalizeGameSetup();
   }

   /// <summary>
   /// finalizes the games setup, but finishing tasks and creating creating
   /// objects like the main menu
   /// </summary>
   private void FinalizeGameSetup()
   {
      // GetViewport().get
      // mainMenuCam.MakeCurrent();


      //figure out where to put maybe spawn in when game worlds created
      // GameClock clock = GameClock.Instance;
      // AddChild(clock);
   }

   public void addPlayer(PlayerCharacter playerCharacter)
   {
      GD.Print("adding child");
      AddChild(playerCharacter);
   }

}
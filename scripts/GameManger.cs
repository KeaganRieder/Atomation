namespace Atomation;

using Atomation.GameMap;
using Atomation.Resources;
using Atomation.Pawns;
using Atomation.Things;
using Godot;
using System.Collections.Generic;
using Atomation.Ui;


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

   private PauseMenu pauseMenu;

   private GameManger() { }

   ~GameManger()
   {
      if (IsInstanceValid(this))
      {
         foreach (var child in GetChildren())
         {
            child.QueueFree();
         }
         QueueFree();
      }
   }


   /// <summary>
   /// runs upon node creation
   /// </summary>
   public override void _Ready()
   {
      base._Ready();
      // ItemDefs.FormatResourceItemDefs();
      // StructureDefs.FormatNaturalStructureDefs();
      TerrainDefs.FormatTerrainDefs();
      LoadResources();

      AddChild(Map.Instance);
      AddChild(Player.Instance);

      pauseMenu = PauseMenu.Instance;
      Player.Instance.GetCamera().AddChild(pauseMenu);
   }

   public static void LoadResources()
   {
      GD.Print("Loading Resources");
      ThingDatabase.Instance.LoadDefs();
      GD.Print("Loading Complete\n");
   }
}
namespace Atomation;

using Atomation.Map;
using Atomation.Resources;
using Atomation.PlayerChar;
using Atomation.Things;
using Godot;
using System.Collections.Generic;

/// <summary>
/// Main class which handles manning the game through different scene
/// </summary>
public partial class GameManger : Node2D
{
   private static GameManger instance;
   

   private WorldMap worldMap;

   private GameManger()
   {
   }
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

   public static GameManger GetInstance()
   {
      if (instance == null)
      {
         instance = new GameManger();
      }
      return instance;
   }

   /// <summary>
   /// runs upon node creation
   /// </summary>
   public override void _Ready()
   {
      base._Ready();
      StructureDefs.FormatNaturalStructureDefs();
      TerrainDefs.FormatTerrainDefs();

      LoadResources();

      worldMap = WorldMap.Instance;

      AddChild(worldMap);

      AddChild(Player.Instance);
   }

   public static void LoadResources()
   {
      GD.Print("Loading Resources");
      DefDatabase.GetInstance();
      GD.Print("Loading Complete\n");
   }
}
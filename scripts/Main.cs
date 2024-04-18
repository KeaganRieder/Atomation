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
public partial class Main : Node2D
{
   public WorldMap Map { get; private set; }
   public FileManger ResourceManger { get; private set; }
   public Player Player { get; private set; }
   public Controller controller { get; private set; }

   public Main()
   {
      ResourceManger = new FileManger();
   }

   /// <summary>
   /// runs upon node creation
   /// </summary>
   public override void _Ready()
   {
      base._Ready();

      ResourceManger.LoadFiles();

      Player Player = new Player();
      Map = new WorldMap(Player);

      controller = new Controller();
      controller.Map = Map;
      controller.PlayerBody = Player;

      AddChild(Map);
      AddChild(Player);
      AddChild(controller);

   }

}

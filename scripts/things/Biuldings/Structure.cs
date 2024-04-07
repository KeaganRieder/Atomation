using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Map;
using Atomation.Resources;

namespace Atomation.Things
{
    /// <summary>
    /// defines basic structures
    /// </summary>
    public partial class Structure : CompThing
    {        
		public FloorGraphic FloorGraphic { get; set; } 

        public Structure(Coordinate coord){
            coordinate = coord;
			Position = coordinate.WorldPosition;

			FloorGraphic = new FloorGraphic(this);
			FloorGraphic.ObjGraphic.VisibilityLayer = 3;
        }

        /// <summary>
		/// reading the configuration data for the given tile
		/// and setting it for anything in which is needing
		/// configuration at current call
		/// </summary>
		public void ReadConfigs(StructureDef config)
		{
			Name = config.Name + Coordinate.ToString();
			Description = config.Description;
			stats = config.FormatStats();
			modifiers = config.FormatStatModifers();
			FloorGraphic.ConfigureGraphic(config.GraphicData);
		}
		
    }
}
using Godot;
using Atomation.Thing;
using Atomation.Resources;


namespace Atomation.Map
{
	/// <summary>
	/// defines the 2nd generation step for generating the terrain.
	/// this include deciding lakes, ocean and where rivers are, mountains,
	/// biomes and general landmasses are
	/// </summary>
	public class GenStepTerrain : GenStep
	{
		// terrain class heights
		private float deepWater;
		private float shallowWater;
		private float shore;
		private float mountain;
		private float rockyGround;

		public GenStepTerrain(MapGenSettings genConfig)
		{

			deepWater = genConfig.seaLevel;
			shallowWater = genConfig.seaLevel + 0.1f;
			shore = genConfig.seaLevel + 0.2f;

			mountain = genConfig.mountainSize;
			rockyGround = genConfig.mountainSize - 0.1f;
		}

		/// <summary>
		/// main function that is called in order to execute step and it's parts
		/// </summary>
		public override void RunStep(Vector2 origin, ChunkHandler chunkHandler)
		{
			for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
			{
				for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
				{
					SampleChunkPos(origin, x, y, out float sampleX, out float sampleY);
					Terrain terrain = chunkHandler.GetTerrain(Mathf.RoundToInt(sampleX), Mathf.RoundToInt(sampleY));

					CreateLandScape(terrain);

					terrain.UpdateGraphic(VisualizationMode.Default);
				}
			}
		}

		/// <summary>
		/// updates a terrain object's moisture,temperature. To now be 
		/// based on what that terrain type is, which is based on it's height
		/// </summary>
		private void CreateLandScape(Terrain terrain)
		{

			if (IsWater(terrain))
			{
				return;
			}
			if (IsMountain(terrain))
			{
				return;
			}
			//it's land so set it to be 
			IsLand(terrain);
		}
		private bool IsWater(Terrain terrain)
		{
			FloorGraphics graphic;
			if (terrain.HeightValue < deepWater)
			{				
				graphic = new FloorGraphics(new Color(0, 0, 0));
				terrain.FloorGraphic = graphic;
				terrain.ThingNode.AddChild(graphic.GetGraphicObj());

				terrain.MoistureValue =1;

				return true;
			}
			else if (terrain.HeightValue < shallowWater)
			{				
				graphic = new FloorGraphics(new Color(0.2f, 0.2f, 0.2f));
				terrain.FloorGraphic = graphic;
				terrain.ThingNode.AddChild(graphic.GetGraphicObj());
			
				terrain.MoistureValue =1;

				return true;
			}
			
			return false;
		}
		private void IsLand(Terrain terrain)
		{
			Color color;
			if (terrain.HeightValue < shore)
			{
				color = new Color(0.4f, 0.4f, 0.4f);
			}
			else
			{
				color = new Color(terrain.HeightValue, terrain.HeightValue, terrain.HeightValue);

			}
			
			FloorGraphics graphic = new FloorGraphics(color);
			terrain.FloorGraphic = graphic;
			terrain.ThingNode.AddChild(graphic.GetGraphicObj());
	
		}
		private bool IsMountain(Terrain terrain)
		{
			FloorGraphics graphic;
			if (terrain.HeightValue > rockyGround && terrain.HeightValue < mountain)
			{
				graphic = new FloorGraphics(new Color(.8f, .8f, .8f));
				terrain.FloorGraphic = graphic;
				terrain.ThingNode.AddChild(graphic.GetGraphicObj());

				return true;
			}
			else if (terrain.HeightValue > mountain)
			{
				graphic = new FloorGraphics(new Color(1, 1, 1));
				terrain.FloorGraphic = graphic;
				terrain.ThingNode.AddChild(graphic.GetGraphicObj());

				return true;
			}
			return false;
		}

	}
}

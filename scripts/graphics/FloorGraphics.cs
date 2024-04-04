using Godot;
using Atomation.Map;

namespace Atomation.Resources
{	
	/// <summary>
	/// decides how to blend seams between two different terrain
	/// and ground types
	/// </summary>
	public enum EdgeType
	{
		Default,
		Rough,
		Smooth,
	}

	/// <summary>
	/// class which defines floor graphics 
	/// </summary>
	public class FloorGraphics : Graphic
	{
		public ColorRect FloorGraphic{get; private set;}

		public FloorGraphics(Node2D terrainNode,Vector2 pos)
		{
			FloorGraphic = new ColorRect();
			FloorGraphic.Size = new Vector2(MapSettings.CELL_SIZE, MapSettings.CELL_SIZE);
			color = new Color(Colors.Black);
			FloorGraphic.Color = color;
            FloorGraphic.VisibilityLayer = 1;

			terrainNode.AddChild(FloorGraphic);
		}

		public override void ConfigureGraphic(GraphicData graphicConfig)
		{
			texturePath = graphicConfig.TexturePath;
			color = graphicConfig.Color;
			color.A = 1;
		}

		/// <summary>
		/// makes terrain display as it's default color/graphic assigned
		/// during gen step terrain
		/// </summary>
		public void DefaultGraphic()
		{
			FloorGraphic.Color = color;
		}
		
		/// <summary>
		/// makes terrain display as heat map
		/// where -.9 is darkRed(hot), and 1 is blue(cold)
		/// </summary>
		public void HeatGraphic(float heatVal)
		{
			Color heatColor;

			if (heatVal < -1.3)
			{
				heatColor = Colors.White;
			}
			else if (heatVal < -1.0)
			{
				heatColor = Colors.Pink;
			}
			else if (heatVal < -0.8)
			{
				heatColor = Colors.Purple;
			}
			else if (heatVal < -0.5)
			{
				heatColor = Colors.DarkBlue;
			}
			else if (heatVal < -0.25)
			{
				heatColor = Colors.Cyan;
			}
			else if (heatVal < 0.25)
			{
				heatColor = Colors.Green;
			}
			else if (heatVal < 0.7)
			{
				heatColor = Colors.DarkGreen;
			}
			else if (heatVal < 1)
			{
				heatColor = Colors.Yellow;
			}
			else if (heatVal < 1.25)
			{
				heatColor = Colors.Orange;
			}
			else if (heatVal < 1.7)
			{
				heatColor = Colors.Red;
			}
			else
			{
				heatColor = Colors.DarkRed;
			}

			FloorGraphic.Color = heatColor;
		}
		
		/// <summary>
		/// makes terrain display as height map
		/// where -1 is black (lowest ground), and 1 is while (hightest ground)
		/// </summary>
		public void HeightGraphic(float heightVal)
		{
			FloorGraphic.Color = new Color(heightVal, heightVal, heightVal);
		}
		/// <summary>
		/// makes terrain display as height map
		/// where -1 is black (lowest ground), and 1 is while (hightest ground)
		/// </summary>
		public void MoistureGraphic(float moistureVal)
		{
			//this needs work
			Color moistureColor;

			if (moistureVal < 0.27)
			{
				moistureColor = Colors.Red;

			}
			else if (moistureVal < 0.4)
			{
				moistureColor = Colors.Orange;

			}
			else if (moistureVal < 0.5)
			{
				moistureColor = Colors.Yellow;

			}
			else if (moistureVal < 0.7)
			{
				moistureColor = Colors.Green;

			}
			else if (moistureVal < 0.8)
			{
				moistureColor = Colors.Cyan;
			}
			else
			{
				moistureColor = Colors.Blue;
			}
			// if (moistureVal < 0.1)
			// {
			// 	moistureColor = Colors.Red;
			// }
			// else if (moistureVal < 0.27)
			// {
			// 	moistureColor = Colors.Orange;
			// }
			// else if (moistureVal < 0.4)
			// {
			// 	moistureColor = Colors.Yellow;
			// }
			// else if (moistureVal < 0.6)
			// {
			// 	moistureColor = Colors.Green;
			// }
			// else if (moistureVal < 0.8)
			// {
			// 	moistureColor = Colors.Blue;
			// }
			// else
			// {
			// 	moistureColor = Colors.DarkBlue;
			// }
			FloorGraphic.Color = moistureColor;
		}
	}
}

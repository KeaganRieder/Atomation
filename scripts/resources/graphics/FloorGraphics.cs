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
	/// abstract class which defines the base of all graphics in Atomation
	/// </summary>
	public class FloorGraphics : Graphic
	{
		private ColorRect floorGraphic;

		public FloorGraphics(GraphicConfig graphicConfig, Node2D terrainNode)
		{

			floorGraphic = new ColorRect();
			floorGraphic.Size = new Vector2(MapSettings.CELL_SIZE, MapSettings.CELL_SIZE);
			floorGraphic.Position = terrainNode.Position;

			terrainNode.AddChild(floorGraphic);

			texturePath = graphicConfig.TexturePath;
			color = graphicConfig.Color;
			color.A = 1;
		}
	 
		public FloorGraphics(Color color)
		{
			this.color = color;
			floorGraphic = new ColorRect();
			floorGraphic.Size = new Vector2(MapSettings.CELL_SIZE, MapSettings.CELL_SIZE);
			this.color.A = 1;
		}

		/// <summary>
		/// gets the graphic node which is currently a ColorRect 
		/// </summary>
		public ColorRect GetGraphicObj()
		{
			return floorGraphic;
		}

		// updating graphic Display
		/// <summary>
		/// makes terrain display as it's default color/graphic assigned
		/// during gen step terrain
		/// </summary>
		public void DefaultGraphic()
		{
			floorGraphic.Color = color;
		}
		/// <summary>
		/// makes terrain display as heat map
		/// where -.9 is darkRed(hot), and 1 is blue(cold)
		/// </summary>
		public void HeatGraphic(float heatVal)
		{
			Color heatColor;

			if (heatVal < -.9f)
			{
				heatColor = Colors.DarkRed;
			}
			else if (heatVal < -.7f)
			{
				heatColor = Colors.Orange;
			}
			else if (heatVal < -.5f)
			{
				heatColor = Colors.Yellow;
			}
			else if (heatVal < -.25f)
			{
				heatColor = Colors.Green;
			}
			else if (heatVal < .1f)
			{
				heatColor = Colors.DarkGreen;
			}
			else if (heatVal < .25f)
			{
				heatColor = Colors.Green;
			}
			else if (heatVal < .5f)
			{
				heatColor = Colors.Cyan;
			}
			else if (heatVal < .7f)
			{
				heatColor = Colors.Blue;
			}
			else if (heatVal < .9f)
			{
				heatColor = Colors.DarkBlue;
			}
			else
			{
				heatColor = Colors.Purple;
			}

			floorGraphic.Color = heatColor;
		}
		/// <summary>
		/// makes terrain display as height map
		/// where -1 is black (lowest ground), and 1 is while (hightest ground)
		/// </summary>
		public void HeightGraphic(float heightVal)
		{
			floorGraphic.Color = new Color(heightVal, heightVal, heightVal);
		}
		/// <summary>
		/// makes terrain display as height map
		/// where -1 is black (lowest ground), and 1 is while (hightest ground)
		/// </summary>
		public void MoistureGraphic(float moistureVal)
		{
			Color moistureColor;
			if (moistureVal < 0.1)
			{
				moistureColor = Colors.Red;
			}
			else if (moistureVal < 0.27)
			{
				moistureColor = Colors.Orange;
			}
			else if (moistureVal < 0.4)
			{
				moistureColor = Colors.Yellow;
			}
			else if (moistureVal < 0.6)
			{
				moistureColor = Colors.Green;
			}
			else if (moistureVal < 0.8)
			{
				moistureColor = Colors.Blue;
			}
			else
			{
				moistureColor = Colors.DarkBlue;
			}
			floorGraphic.Color = moistureColor;
		}

	}
}

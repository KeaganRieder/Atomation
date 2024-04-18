using System;
using System.Collections.Generic;
using Godot;
using Atomation.Things;
using Atomation.Resources;

namespace Atomation.Map
{
	/// <summary>
	/// A chunk is a 32 x 32 tiles section of the map that contains
	/// various values, it is either loaded or unloaded depending on where 
	/// the player is, as well as other aspects
	/// </summary>
	public partial class Chunk : Node2D
	{
		/// <summary>
		/// the length and width of a chunk in terms of pixels.
		/// </summary>
		public const int CHUNK_SIZE = 32;

		public float cellSize { get; private set; }
		public bool Rendered { get; private set; }
		public Coordinate coordinate{ get; private set; }

		public Grid<Terrain> Terrain { get; private set; }
		public Grid<Structure> Buildings { get; private set; }

		public Chunk(Vector2 worldPosition, float cellSize)
		{
			Name = $"Chunk {worldPosition/CHUNK_SIZE}";
			
			this.cellSize = cellSize;
			coordinate = new Coordinate(worldPosition);
			Rendered = true;
			
			Terrain = new Grid<Terrain>(CHUNK_SIZE, CHUNK_SIZE, cellSize, worldPosition,this);
			Buildings = new Grid<Structure>(CHUNK_SIZE, CHUNK_SIZE, cellSize, worldPosition,this);
		}

		/// <summary>
		/// updates the visualization mode of all tiles
		/// </summary>
		public void UpdateTerrainVisualization(VisualizationMode displayMode)
		{
			// Terrain
			for (int x = 0; x < CHUNK_SIZE; x++)
			{
				for (int y = 0; y < CHUNK_SIZE; y++)
				{
					Terrain.GetObject(x, y).UpdateGraphic(displayMode);
				}
			}
		}

		/// <summary>
		/// checks viewer distance from chunk and then decided based on rendered distance
		/// to decide weather or not to hide/un render chunk.
		/// </summary>
		public void UpdateVisibility(Coordinate viewerCords)
		{
			bool visible = coordinate.Distance(viewerCords) <=MapSettings.MAX_LOAD_DIST;
			
			SetVisibility(visible);
		}

		public void SetVisibility(bool visible)
		{
			Rendered = visible;

			Visible = Rendered;
		}
	}
}

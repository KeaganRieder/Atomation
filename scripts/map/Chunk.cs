using System;
using System.Collections.Generic;
using Godot;
using Atomation.Thing;
using Atomation.Resources;

namespace Atomation.Map
{
	/// <summary>
	/// A chunk is a 32 x 32 section of the map that contains
	/// various values, it is either loaded or unloaded depending on where 
	/// the player is, as well as other aspects
	/// </summary>
	public partial class Chunk : Node2D
	{
		public const int CHUNK_SIZE = 32;
		public float CellOffset { get; private set; }
		public bool Rendered { get; private set; }
		public Vector2 Origin { get; private set; }

		public Grid<Terrain> Terrain { get; private set; }
		public Grid<Terrain> Buildings { get; private set; }

		public Chunk() { }

		public Chunk(Vector2 worldPosition, float cellSize) : this()
		{
			Name = $"Chunk {worldPosition}";

			CellOffset = cellSize;
			Origin = worldPosition;
			Position = Origin;
			Rendered = true;

			Terrain = new Grid<Terrain>(CHUNK_SIZE, CHUNK_SIZE, cellSize, Origin, this);
			Buildings = new Grid<Terrain>(CHUNK_SIZE, CHUNK_SIZE, cellSize, Origin, this);
		}

		/// <summary>
		/// gets distance form chunks top left (if negative y) or bot left (if positive y)
		/// corner cords to the provided at worldPosition
		/// </summary>
		private float GetDistance(Vector2 worldPosition)
		{
			worldPosition = new Vector2(Mathf.FloorToInt(worldPosition.X / CellOffset), Mathf.FloorToInt(worldPosition.Y / CellOffset));

			//align chunk to the pixel gird
			Vector2 chunkPos = Origin / CellOffset;
			Vector2 chunkDistance = chunkPos - worldPosition;

			int distance = Mathf.FloorToInt(Mathf.Min(Mathf.Abs(chunkDistance.X), Mathf.Abs(chunkDistance.Y)));

			return distance;
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
		public void UpdateVisibility(Vector2 viewerCords)
		{
			//if within render bounds then keep rendered otherwise un render it
			bool visible = GetDistance(viewerCords) <= MapSettings.MAX_LOAD_DIST;
			
			SetVisibility(visible);
		}

		public void SetVisibility(bool visible)
		{
			Rendered = visible;

			Visible = Rendered;
		}
	}
}

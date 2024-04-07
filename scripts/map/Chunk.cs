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
		/// gets distance form chunks top left (if negative y) or bot left (if positive y)
		/// corner cords to the provided at worldPosition
		/// </summary>
		private float GetDistance(Vector2 worldPosition)
		{
			//this needs fixing
			worldPosition = new Vector2(Mathf.FloorToInt(worldPosition.X / cellSize), Mathf.FloorToInt(worldPosition.Y / cellSize));

			//align chunk to the pixel gird
			Vector2 chunkPos = coordinate.WorldPosition / cellSize;
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

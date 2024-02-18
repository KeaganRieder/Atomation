using System;
using System.Collections.Generic;
using Godot;
using Atomation.Thing;
namespace Atomation.Map
{
	/// <summary>
	/// A chunk is a 32 x 32 section of the map that contains
	/// various values, it is either loaded or unloaded depending on where 
	/// the player is, as well as other aspects
	/// </summary>
	public class Chunk
	{
		public const int CHUNK_SIZE = 32;

		private Node2D chunkNode;

		private Dictionary<Vector2, Terrain> chunkTerrain; // maybe make new class for this?

		//constructors
		public Chunk()
		{
			chunkTerrain = new Dictionary<Vector2, Terrain>();
		}

		public Chunk(Vector2 chunkCords, Node2D parentNode) : this()
		{
			//make object aligned to cell size grid also
			Vector2 Cords = chunkCords * WorldMap.CELL_SIZE;
			
			chunkNode = new Node2D()
			{
				Name = $"Chunk {chunkCords}",
				Position = Cords,
			};

			parentNode.AddChild(chunkNode);
		}

		//getters and setters 
		public Node2D ChunkNode { get => chunkNode; }

		/// <summary>
		/// sets the terrain at the provided cords
		/// </summary>
		public void Set(Vector2 cords, Terrain terrain)
		{
			chunkTerrain[cords] = terrain;
			chunkNode.AddChild(chunkTerrain[cords].Node);
		}

		/// <summary>
		/// gets the terrain at the provided cords
		/// </summary>
		public Terrain GetTerrain(Vector2 cords)
		{
			if (chunkTerrain.ContainsKey(cords))
			{
				return chunkTerrain[cords];
			}
			return null;
		}

		//
		//rendering stuff
		//
		
		public void UpdateChunk(Vector2 viewerCords)
		{
			float distToViewer = (chunkNode.Position / CHUNK_SIZE).DistanceTo(viewerCords);
			bool visible = distToViewer <= ChunkHandler.MAX_LOAD_DIST;
			SetRenderState(visible);
		}
		public bool Rendered()
		{
			return ChunkNode.Visible;
		}
		public void SetRenderState(bool state)
		{
			ChunkNode.Visible = state;
		}

		//todo make show grid 
	}
}

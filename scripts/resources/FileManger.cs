using Godot;
using System;
using System.Collections.Generic;
using Atomation.Thing;

namespace Atomation.Resources
{
	/// <summary>
	/// handles loading/managing file and general resource access.
	/// </summary>
	public partial class FileManger
	{
		public const string DEF_FOLDER = "data/core/defs/";
		public const string TEXTURE_FOLDER = "resources/textures";
		public const string AUDIO_FOLDER = "resources/audio";
		public const string CONFIGS = "data/configs/";
		public const string BINDINGS_FOlDER = "data/key_bindings/";

		public FileManger()
		{
		}

		/// <summary>
		/// loads files, this function is generally called
		/// during start up
		/// </summary>
		public void LoadFiles()
		{
			GD.Print("Loading Resources");
			DefDatabase.LoadResources();
			GD.Print("Loading Complete");
		}

	}
}
namespace Atomation.Resources
{
    /// <summary>
    /// class which contain constants for important file paths in 
    /// for the game
    /// </summary>
    public static class FilePath
    {
        /// <summary>
        /// data/save_games/
        /// </summary>
        public const string SAVE_FOLDER = "data/save_games/";

        /// <summary>
        /// data/core/defs/
        /// </summary>
        public const string DEFINITION_FOLDER = "data/core/defs/";
        /// <summary>
        /// data/core/defs/terrain/
        /// </summary>
        public const string TERRAIN_FOLDER = DEFINITION_FOLDER + "terrain/";
        /// <summary>
        /// data/core/defs/structures/
        /// </summary>
        public const string STRUCTURE_FOLDER = DEFINITION_FOLDER + "structures/";
        /// <summary>
        /// data/core/defs/biomes/
        /// </summary>
        public const string BIOME_FOLDER = DEFINITION_FOLDER + "biomes/"; 

        /// <summary>
        /// data/core/defs/
        /// </summary>
        public const string CONFIG_FOLDER = "data/configs/";

        /// <summary>
        /// data/core/defs/key_bindings/
        /// </summary>
        public const string KEYBINDINGS_FOLDER = CONFIG_FOLDER + "key_bindings/";
        /// <summary>
        /// asset/
        /// </summary>
        public const string Asset_FOLDER = "asset/";

    }
}
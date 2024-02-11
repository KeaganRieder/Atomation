using Godot;
using System;
using System.IO;
using Newtonsoft.Json;

namespace Atomation.ResHandling
{
    /// <summary>
    /// defines a class which handles the writing of 
    /// json files. theres only one type of this class
    ///  so it's static
    /// </summary>
    public static class JsonWriter
    {
        /// <summary>
        /// write a json file using provided settings
        /// </summary>
        public static void WriteFile(string FolderPath, string fileName, object obj, JsonSerializerSettings settings)
        {
            string directory = ProjectSettings.GlobalizePath(FolderPath);

            string path = directory + fileName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Write(path, obj, settings);
        }

        /// <summary>
        /// write a json file using default settings
        /// </summary>
        public static void WriteFile(string FolderPath, string fileName, object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            string directory = ProjectSettings.GlobalizePath(FolderPath);
            string path = directory + fileName;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            Write(path, obj, settings);
        }

        private static void Write(string filePath, object obj, JsonSerializerSettings settings)
        {
            string data = JsonConvert.SerializeObject(obj, settings);
            try
            {
                File.WriteAllText(filePath, data);
            }
            catch (Exception error)
            {
                GD.PrintErr("Error writing file: " + error.Message);
            }
        }

    }
}
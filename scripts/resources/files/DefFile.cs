using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using Atomation.Thing;

namespace Atomation.Resources
{
    public class DefFile<defType> where defType : IThing
    { 
        public Dictionary<string, defType> Defs;
    
        public DefFile(){}
        
        /// <summary>
        /// constructor which is used primarily for formatting new def file
        /// </summary>
        public DefFile(Dictionary<string, defType> contents, string path, string fileName ){
            Defs = contents;
            FileManger.WriteJsonFile(path, fileName, this);
        }

        /// <summary>
        /// constructor when called reads in def files from given
        /// folder path
        /// </summary>
        public DefFile(string folderPath)
        {
            Defs = new Dictionary<string, defType>();
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string filePath in files)
                {
                    DefFile<defType> defFile = FileManger.ReadJsonFile<DefFile<defType>>(filePath);
                    foreach (var def in defFile.Defs)
                    {
                        CacheFileData(def.Key, def.Value);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException($"file read failed: {folderPath} missing");
            }
        }

        public void FormatFile(string path, string fileName){
            FileManger.WriteJsonFile(path, fileName, this);
        }

        public void CacheFileData(string name, defType obj)
        {
            try
            {
                Defs.Add(name, obj);
            }
            catch (Exception Error)
            {
                GD.PushError($"Error failed to add key: {Error.Message}");
            }
        }

        public defType this[string key]
        {
            get
            {
                if (Defs.ContainsKey(key))
                {
                    return Defs[key];
                }
                else
                {
                    throw new KeyNotFoundException($"Key '{key}' not found in the dictionary.");
                }
            }
        }
    }
}
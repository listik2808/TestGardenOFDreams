using System.IO;
using UnityEngine;

namespace Scripts.Infrastructure.Services.SaveLoade
{
    public static class SaveLoad 
    {
        private static readonly string SaveFolder = Application.dataPath + "/Save";

        public static void Init() 
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
        }

        public static void Save(string save)
        {
            File.WriteAllText(SaveFolder + "/save.txt", save);
        }

        public static string Load()
        {
            if(File.Exists(SaveFolder + "/save.txt"))
            {
                string save = File.ReadAllText(SaveFolder + "/save.txt");
                return save;
            }
            else
            {
                return null;
            }
        }
    }
}
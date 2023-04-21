using System;

namespace Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public NameSceneI LevelScene;

        public WorldData(string initialLevel)
        {
            LevelScene = new NameSceneI(initialLevel);
        }
    }
}

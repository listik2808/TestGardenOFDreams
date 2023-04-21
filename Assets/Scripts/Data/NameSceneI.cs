using System;

namespace Scripts.Data
{
    [Serializable]
    public class NameSceneI
    {
        public string Level;

        public NameSceneI(string initialLevel)
        {
            Level = initialLevel;
        }
    }
}
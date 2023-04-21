using UnityEngine;

namespace Scripts.Data
{
    public static class DataExtensions
    {
        public static NameSceneI AsStringData(this string name)=>
            new NameSceneI(name);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj); 

        public static T ToDeserialized<T>( this string json ) =>
            JsonUtility.FromJson<T>(json);
    }
}

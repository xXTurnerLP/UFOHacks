using UnityEngine;
using System.Runtime.CompilerServices;

namespace UFO
{
    public class Loader
    {
        public static void Inject()
        {
            GameObject obj = new GameObject();
            obj.AddComponent<Manager>();
            Object.DontDestroyOnLoad(obj);
        }
    }
}
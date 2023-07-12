using Unity.Netcode;
using UnityEngine;


namespace Singletons // A version of DilmerGames.Core.Singleton
{
    public class Singleton<T> : NetworkBehaviour
        where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    //Debug.Log("_instance == null");
                    
                    var objs = FindObjectOfType(typeof(T)) as T[];
                    
                    Debug.Log(objs.ToString());
                    if (objs != null)
                    {
                       // Debug.Log("Existe " + typeof(T).Name + " in the scene");
                        if (objs.Length > 0)
                        {
                         //   Debug.Log("Adding to singleton");
                            _instance = objs[0];
                        }
                        if (objs.Length > 1)
                        {
                            Debug.LogError("Too many " + typeof(T).Name + "in the scene");
                        }
                       
                    } else
                    {
                        if (_instance == null)
                        {
                            //Debug.Log("New  singleton");
                            GameObject obj = new GameObject();
                            obj.name = string.Format("{0}", typeof(T).Name);
                            _instance = obj.AddComponent<T>();
                        }
                    }

                }
                return _instance;
            }

        }
    }

}

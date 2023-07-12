using Unity.Netcode;
using UnityEngine;

public class SingletonA<T>: NetworkBehaviour
    where T: Component
{
    public static T Instance { get; private set; }

    public void Awake()
    {
        var comp = GetComponent<T>();
        if(comp != null)
        {
            Debug.Log("Singleton PlayersManager ready...");
            Instance = comp;
        } else
        {
            Debug.LogError("Error al crear el singleton");
        }
    }

}
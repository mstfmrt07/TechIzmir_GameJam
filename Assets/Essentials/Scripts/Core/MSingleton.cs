using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = new GameObject(nameof(T)).AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}

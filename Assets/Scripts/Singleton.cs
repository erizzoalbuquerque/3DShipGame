using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    static T _instance;

    static public T Instance  {
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
            }
            return _instance;
        } 
    }
}

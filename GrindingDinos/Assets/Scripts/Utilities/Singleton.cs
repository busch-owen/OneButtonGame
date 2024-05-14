using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T _instance;

    // Constructor method using a getter for the Instance
    public static T Instance
    {
        get
        {
            if ( _instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    SetupInstance();
                }
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        RemoveDuplicates();
    }

    // Creates the instance of a Child Singleton class
    static void SetupInstance()
    {
        _instance = (T)FindObjectOfType(typeof(T));

        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            _instance = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }

    // This function will delete any duplicates found of a same singleton derivative type
    // For example if there are 2 GameManagers one will be deleted so there is only one in existence
    void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

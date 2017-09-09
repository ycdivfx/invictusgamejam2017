using System;
using UnityEngine;


[AttributeUsage(AttributeTargets.Class)]
public class SingletonDontDestroyAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class)]
public class SingletonDontCreateAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class)]
public class SingletonEnableOnAccess : Attribute
{
}

public abstract class Singleton : MonoBehaviour
{
    protected static bool Instantiated;

    internal void OnDestroy()
    {
        Instantiated = false;
    }
}

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    private static T _instance;
    private static bool _dontEnableLookup;
    private static bool _dontEnable;

    public static T Instance
    {
        get
        {
            if (Instantiated)
            {
                if (_instance != null)
                {
                    // Cache dont enable to avoid expensive attribute lookups.
                    if (!_dontEnableLookup)
                    {
                        _dontEnable = Attribute.GetCustomAttribute(typeof(T), typeof(SingletonEnableOnAccess)) is SingletonEnableOnAccess;
                        _dontEnableLookup = true;
                    }
                    if (!_instance.gameObject.activeSelf && _dontEnable)
                        _instance.gameObject.SetActive(true);
                    return _instance;
                }

                Instantiated = false;
            }

            var type = typeof(T);
            var objects = FindObjectsOfType<T>();

            var dontDestroy = Attribute.GetCustomAttribute(type, typeof(SingletonDontDestroyAttribute)) is SingletonDontDestroyAttribute;
            var dontCreate = Attribute.GetCustomAttribute(type, typeof(SingletonDontCreateAttribute)) is SingletonDontCreateAttribute;

            if (objects.Length > 0)
            {
                Instance = objects[0];
                if (objects.Length > 1)
                {
                    Debug.LogWarning("There is more than one instance of Singleton of type \"" + type +
                                     "\". Keeping the first. Destroying the others.");
                    for (var i = 1; i < objects.Length; i++) DestroyImmediate(objects[i].gameObject);
                }
                Instantiated = true;

                if(dontDestroy)
                    DontDestroyOnLoad(_instance);

                return _instance;
            }

            if (dontCreate)
                return null;

            var singleton = new GameObject();
            Instance = singleton.AddComponent<T>();
            singleton.name = typeof(T).Name;

            if (dontDestroy)
                DontDestroyOnLoad(singleton);

            return _instance;
        }

        private set
        {
            _instance = value;
            Instantiated = value != null;
        }
    }
}
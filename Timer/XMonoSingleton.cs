using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// X Mono singleton.
/// </summary>
public abstract class XMonoSingleton<T> : MonoBehaviour where T : XMonoSingleton<T>
{
    private static T instance;
    private static readonly object instanceLock = new object();
    private static bool quitFlag = false;

    public static T Instance
    {
        get
        {
            if (quitFlag)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                return null;
            }
            Init();
            return instance;
        }
    }

    /// <summary>
    /// init
    /// </summary>
    public static void Init()
    {
        lock (instanceLock)
        {
            if (instance == null)
            {
                // Search for existing instance.
                instance = (T)FindObjectOfType(typeof(T));

                // Create new instance if one doesn't already exist.
                if (instance == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.GetOrAddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        quitFlag = true;
    }

    private void OnDestroy()
    {
        quitFlag = true;
    }
}

/// <summary>
/// X Singleton.
/// </summary>
public abstract class XSingleton<T> where T : class
{
    class Nested
    {
        internal static readonly T instance = Activator.CreateInstance(typeof(T), true) as T;
    }
    public static T Instance { get { return Nested.instance; } }
}

/// <summary>
/// app status
/// </summary>
public class AppStatus : MonoBehaviour
{
    static AppStatus()
    {
        IsAppQuit = false;
    }

    public static bool IsAppQuit { get; private set; }

    private void OnApplicationQuit()
    {
        IsAppQuit = true;
    }
}

/// <summary>
/// Unity engine extensions
/// </summary>
public static class UnityEngineExtensions
{
    /// <summary>
    /// Returns the component of Type type. If one doesn't already exist on the GameObject it will be added.
    /// </summary>
    /// <typeparam name="T">The type of Component to return.</typeparam>
    /// <param name="gameObject">The GameObject this Component is attached to.</param>
    /// <returns>Component</returns>
    static public T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    }
}
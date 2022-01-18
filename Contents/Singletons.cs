using UnityEngine;

/// <summary>
/// A static instance is similar to a singleton. 
/// However, instead of destroying new instances, it simply overrides the current one.
/// This is useful when wanting to reset the state of a single-instance object.
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// A transformer for the <see cref="StaticInstance{T}"/> class, turning it into a proper singleton.
/// It will destroy any new instances of its class, conserving the original instance and state.
/// <para>
/// Note: This singleton is not persistent throughout scenes. For this, use the 
/// <seealso cref="PersistentSingleton{T}"/> class.
/// </para>
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        base.Awake();
    }
}

/// <summary>
/// A persistent <see cref="Singleton{T}"/>. It will persist through scene changes.
/// Used when persistent data, systems, or states are needed across scenes.
/// <para>
/// Typical use cases:
/// <list type="bullet">
/// <item>Tracking game state</item>
/// <item>Playing audio between scenes</item>
/// </list>
/// </para>
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
using UnityEngine;

public abstract class PersistentSingleton<T> : MonoBehaviour where T: PersistentSingleton<T>
{
    public bool Persistent = true;

    static public T Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (Persistent)
            DontDestroyOnLoad(this.gameObject);
    }
}

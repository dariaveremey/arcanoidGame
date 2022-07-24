using UnityEngine;

public class SingletonMonoBehavior<T> : MonoBehaviour
{
    #region Variables

    public static T Instance { get; private set; }

    #endregion


    #region Unity lyfecycle

    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}
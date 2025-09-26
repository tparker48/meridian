using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static GameObject instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject); // preserve between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

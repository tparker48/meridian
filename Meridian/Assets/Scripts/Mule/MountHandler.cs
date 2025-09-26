using UnityEngine;

public class MountHandler : MonoBehaviour
{    
    public void Mount()
    {
        PlayerSingleton.instance.GetComponent<PlayerMovement>().Mount(transform.parent.parent.gameObject);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableEntity : MonoBehaviour
{
    public void OnRecieveShot()
    {
        Debug.Log($"Shot! ({gameObject.name})");
    }
}

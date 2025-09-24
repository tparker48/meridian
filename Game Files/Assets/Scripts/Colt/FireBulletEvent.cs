using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletEvent : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHammerDown()
    {
        int bullets = animator.GetInteger("bullets");

        if (bullets > 0)
        {
            Debug.Log($"Shot Fired! ({bullets-1} left)");
            animator.SetInteger("bullets", bullets - 1);
        }
        else
        {
            Debug.Log("Click!");
        }
        
    }
}

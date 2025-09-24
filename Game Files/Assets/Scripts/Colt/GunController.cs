using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Shoot Trigger!");
                animator.SetTrigger("shoot");
            }
            else if (Input.GetKeyDown(KeyCode.R) && animator.GetInteger("bullets") < 6)
            {
                Debug.Log("Reload!");
                animator.SetTrigger("reload");
            }
            
        }
    }
}

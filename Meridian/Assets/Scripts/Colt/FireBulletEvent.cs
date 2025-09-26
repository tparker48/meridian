using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletEvent : MonoBehaviour
{
    public float shotDistance;

    private Animator animator;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Hitbox");
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
            // fire
            ShootBullet();
            animator.SetInteger("bullets", bullets - 1);
        }
        else
        {
            // dry fire
        }

    }

    private void ShootBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, shotDistance, layerMask))
        {
            ShootableEntity shotTarget = hit.transform.gameObject.GetComponentInParent<ShootableEntity>();
            if (shotTarget != null)
            {
                shotTarget.OnRecieveShot();
            }
        }
    }
}

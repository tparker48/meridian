using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Animations;

public enum AnimatorAction
{
    Idle,
    Walking
}

public class GenericSpriteAnimator : MonoBehaviour
{
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    public List<AnimationClip> Idle;
    public List<AnimationClip> Walking;

    public AnimatorAction action = 0;
    public Face face = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["Idle Front"] = Idle[0];
        animatorOverrideController["Idle Side"] = Idle[1];
        animatorOverrideController["Idle Back"] = Idle[2];

        animatorOverrideController["Walking Front"] = Walking[0];
        animatorOverrideController["Walking Side"] = Walking[1];
        animatorOverrideController["Walking Back"] = Walking[2];
    }

    void Update()
    {
        UpdateFace();
        GetComponent<Animator>().SetInteger("face", (int)face);
        GetComponent<Animator>().SetInteger("action_id", (int)action);
    }

    private void UpdateFace()
    {
        Vector3 forward = transform.parent.transform.forward;
        Vector3 toCam = Camera.main.transform.position - transform.position;
        Debug.DrawLine(transform.position, transform.position + forward);
        Debug.DrawLine(transform.position, transform.position + toCam);
        forward.y = 0;
        toCam.y = 0;
        float angle = Vector3.SignedAngle(forward, toCam, Vector3.up);

        transform.localRotation = Quaternion.Euler(0, 180, 0);

        GetComponent<SpriteRenderer>().flipX = false;

        if (-45.0 <= angle && angle <= 45.0)
        {
            // Front
            face = Face.Front;
            return;
        }
        else if (45.0 <= angle && angle <= 135.0)
        {
            // Left
            transform.Rotate(0, 90, 0);
            face = Face.Side;
            return;

        }
        else if (-135.0 <= angle && angle <= -45.0)
        {
            // Right
            transform.Rotate(0, -90, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            face = Face.Side;
            return;
        }
        else
        {
            // Back
            transform.Rotate(0, 180, 0);
            face = Face.Back;
            return;
        }
    }
}

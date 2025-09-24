using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Animations;

public enum Face
{
    Front,
    Side,
    Back
}

public class SpriteAnimator : MonoBehaviour
{
    public int actionId = 0;


    void Update()
    {
        GetComponent<Animator>().SetInteger("side", (int)GetFace());
        GetComponent<Animator>().SetInteger("action_id", actionId);

        //transform.LookAt(Camera.main.transform, Vector3.up);
        //transform.Rotate(0, 180, 0);
        //Vector3 rotation = transform.rotation.eulerAngles;
        //rotation.x = 0;
        //rotation.z = 0;
        //transform.rotation = Quaternion.Euler(rotation);


    }

    private Face GetFace()
    {
        Vector3 forward = transform.parent.transform.forward;
        Vector3 toCam = transform.position - Camera.main.transform.position;
        forward.y = 0;
        toCam.y = 0;
        float angle = Vector3.SignedAngle(forward, toCam, Vector3.up);

        transform.localRotation = Quaternion.Euler(0,0,0);

        GetComponent<SpriteRenderer>().flipX = false;

        if (-45.0 <= angle && angle <= 45.0)
        {
            return Face.Front;
        }
        else if (45.0 <= angle && angle <= 135.0)
        {
            transform.Rotate(0, 90, 0);
            return Face.Side;

        }
        else if (-135.0 <= angle && angle <= -45.0)
        {
            transform.Rotate(0, 90, 0);
            //GetComponent<SpriteRenderer>().flipX = true;
            return Face.Side;
        }
        else
        {
            return Face.Back;
        }        
    }
}

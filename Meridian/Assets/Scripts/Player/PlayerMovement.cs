using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool mounted = false;
    public GameObject mountedMuleObject;
    
    public Animator gunAnimator;
    public Animator cameraAnimator;
    public Animator horseAnimator;

    public float walkMultiplier = 2.5f;
    public float sprintMultiplier = 4.0f;

    public float horseWalkMultiplier = 3.5f;
    public float horseSprintMultiplier = 5.0f;

    public float horseWalkTurnMultiplier = 25.0f;
    public float horseSprintTurnMultiplier = 15.0f;

    public float mouseSensitivity = 1.0f;

    private float yaw, pitch = 0.0f;

    private CharacterController controller;

    private bool sprintInput = false;
    private bool isCursorLocked = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        ToggleCursorState();
    }

    void Update()
    {
        sprintInput = Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0.0f;

        AnimatorUpdate();
        CamUpdate();

        if (mounted)
        {
            RideUpdate();
        }
        else
        {
            WalkUpdate();
        }
    }

    void AnimatorUpdate()
    {
        mountedMuleObject.SetActive(mounted);
        cameraAnimator.SetBool("mounted", mounted);
        cameraAnimator.SetBool("sprinting", sprintInput);
        horseAnimator.SetBool("sprinting", mounted && sprintInput);
        horseAnimator.SetBool("walking", mounted && (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f));
        gunAnimator.SetBool("sprinting", !mounted && sprintInput); 
    }

    void RideUpdate()
    {
        // turning
        float turn = Input.GetAxis("Horizontal");
        float turnSpeed = sprintInput ? horseSprintTurnMultiplier : horseWalkTurnMultiplier;
        transform.Rotate(new Vector3(0.0f, turn * turnSpeed * Time.deltaTime ,0.0f));

        // galloping
        float gallop = Input.GetAxis("Vertical");
        float inputSpeed = gallop;
        if (inputSpeed < 0.0f) {
            inputSpeed *= 0.3f;
        }
        if (sprintInput)
        {
            inputSpeed *= horseSprintMultiplier;
        }
        else
        {
            inputSpeed *= horseWalkMultiplier;
        }
        controller.SimpleMove(inputSpeed  * transform.forward.normalized);
    }

    void WalkUpdate() {
        // WASD Movement
        Vector3 direction = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        float inputSpeed = Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetAxis("Vertical") < 0.0f)
        {
            inputSpeed *= 0.7f;
        }
        if (gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sprinting"))
        {
            inputSpeed *= sprintMultiplier;
        }
        else
        {
            inputSpeed *= walkMultiplier;
        }
        controller.SimpleMove(inputSpeed * direction.normalized);
    }

    void CamUpdate(){
        // Cursor Lock / Unlock
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            isCursorLocked = !isCursorLocked;
            ToggleCursorState();
        }

        // Mouse Look
        if (isCursorLocked)
        {
            yaw += mouseSensitivity * Input.GetAxis("Mouse X");
            pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");

            // rotate full character if not on horse
            if (mounted)
            {
                pitch = Mathf.Clamp(pitch, -89f, 89f);
                yaw = Mathf.Clamp(yaw, -89f, 89f);
                Camera.main.transform.eulerAngles = transform.eulerAngles;
                Camera.main.transform.Rotate(pitch, yaw, 0.0f);
            }
            else
            {
                pitch = Mathf.Clamp(pitch, -89f, 89f);
                Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, yaw, transform.eulerAngles.z);
            }
        }
    }

    private void ToggleCursorState()
    {
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCursorLocked;
    }
}

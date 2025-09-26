using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float detectionDistance = 1.5f;
    public TextMeshProUGUI interactionMessageText;

    private Interactable detectedInteractable;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Interaction");
    }

    // Update is called once per frame
    void Update()
    {
        interactionMessageText.text = "";

        // Find interactables
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth*0.5f, Camera.main.pixelHeight*0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, detectionDistance, layerMask))
            {
                detectedInteractable = hit.transform.gameObject.GetComponent<Interactable>();
            }
            else
            {
                detectedInteractable = null;
            }


        // Respond to keypress
        if (detectedInteractable != null)
        {
            // Display Prompt
            interactionMessageText.text = $"[F] {detectedInteractable.GetInteractionPrompt()}";

            // Handle Keypress
            if (Input.GetKeyUp(KeyCode.F))
            {
                detectedInteractable.Interact();
            }
        }

    }
}

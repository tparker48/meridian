using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Interactable detectedInteractable;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Find interactables
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth*0.5f, Camera.main.pixelHeight*0.5f, 0.0f));
        Debug.DrawLine(ray.origin, ray.origin+(ray.direction * 2.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.0f) && hit.transform.gameObject.CompareTag("Interactable"))
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
            // ...
            Debug.Log(detectedInteractable.GetInteractionPrompt());

            // Handle Keypress
            if (Input.GetKey(KeyCode.F))
            {
                detectedInteractable.Interact();
            }
        }

    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public string actionName;

    public string GetInteractionPrompt()
    {
        return actionName;
    }

    public void Interact()
    {
        onInteract.Invoke();
    }
}

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // Essential for the New Input System

public class OnboardingController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueCanvas; // Drag your Canvas here in the Inspector
    
    [Header("Settings")]
    public string[] lines;
    public float typeSpeed = 0.05f;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        // We start with the canvas off; OnTriggerEnter will turn it on
        if(dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }

    void Update()
    {
        // Only listen for clicks if the dialogue is actually visible
        if (dialogueCanvas != null && dialogueCanvas.activeSelf)
        {
            // The New System check for Mouse, Space, or VR Trigger
            if ((Pointer.current != null && Pointer.current.press.wasPressedThisFrame) || 
                (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame))
            {
                HandleInput();
            }
        }
    }

    void HandleInput()
    {
        if (dialogueText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            // If the bear is still typing, finish the line instantly
            StopAllCoroutines();
            dialogueText.text = lines[index];
            isTyping = false;
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // End of this part of the story
            Debug.Log("Onboarding Phase 1 Finished!");
            dialogueCanvas.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Player enters the Bear's circle
        if (other.CompareTag("MainCamera") || other.name.Contains("XR"))
        {
            if (dialogueCanvas != null && !dialogueCanvas.activeSelf)
            {
                dialogueCanvas.SetActive(true);
                index = 0; // Reset to start
                StartCoroutine(TypeLine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Hide the text if the player walks away
        if (other.CompareTag("MainCamera") || other.name.Contains("XR"))
        {
            if(dialogueCanvas != null)
                dialogueCanvas.SetActive(false);
            StopAllCoroutines();
        }
    }
}
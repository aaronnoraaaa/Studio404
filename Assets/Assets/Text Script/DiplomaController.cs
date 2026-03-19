using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

public class DiplomaController : MonoBehaviour
{
    [Header("Setup")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // Link Diploma here
    public ParticleSystem confetti;             // Link ConfettiBurst here
    public Renderer diplomaRenderer;           // Link Diploma mesh here

    [Header("Lightstick Settings")]
    [ColorUsage(true, true)] public Color activeGlowColor = Color.yellow * 4f;
    private Color offColor = Color.black;
    private bool isGlowEnabled = false;

    void Update()
    {
        // SAFETY: Only works if you are holding the diploma
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            HandleActionInput();
        }
    }

    void HandleActionInput()
    {
        if (Pointer.current == null) return;

        // 1. CONFETTI BLAST (Trigger / Left Click)
        if (Pointer.current.press.wasPressedThisFrame)
        {
            if (confetti != null) confetti.Play();
        }

        // 2. LIGHTSTICK TOGGLE (Grip / 'G' Key)
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            ToggleLightstick();
        }
    }

    void ToggleLightstick()
    {
        isGlowEnabled = !isGlowEnabled;
        
        if (diplomaRenderer != null)
        {
            Color targetColor = isGlowEnabled ? activeGlowColor : offColor;
            diplomaRenderer.material.SetColor("_EmissionColor", targetColor);
        }
    }
}
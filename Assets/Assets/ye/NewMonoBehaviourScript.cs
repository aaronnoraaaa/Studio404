using UnityEngine;

public class BearInteraction : MonoBehaviour
{
    private Animator bearAnimator;
    private Transform playerCamera;
    private bool isPlayerNearby = false;

    void Start()
    {
        bearAnimator = GetComponent<Animator>();
        // Find the camera automatically
        playerCamera = Camera.main.transform; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.name.Contains("XR"))
        {
            isPlayerNearby = true;
            bearAnimator.SetTrigger("StandUp");
            bearAnimator.ResetTrigger("SitDown"); // Clear any old sit requests
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.name.Contains("XR"))
        {
            isPlayerNearby = false;
            bearAnimator.SetTrigger("SitDown");
            bearAnimator.ResetTrigger("StandUp");
        }
    }

    // This handles the "Look At" behavior using Unity's IK system
    private void OnAnimatorIK(int layerIndex)
    {
        if (bearAnimator)
        {
            if (isPlayerNearby)
            {
                // 1.0 means full weight (look directly at player)
                bearAnimator.SetLookAtWeight(1.0f);
                bearAnimator.SetLookAtPosition(playerCamera.position);
            }
            else
            {
                // 0.0 means return to natural animation pose
                bearAnimator.SetLookAtWeight(0.0f);
            }
        }
    }
}
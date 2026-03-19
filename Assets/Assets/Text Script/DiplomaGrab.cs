using UnityEngine;

public class DiplomaGrab : MonoBehaviour
{
    public Transform handAnchor; // Drag 'DiplomaAnchor' from Right Hand here
    private bool isHeld = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing touching the diploma is the Controller
        if (other.gameObject.CompareTag("Player") || other.name.Contains("Hand"))
        {
            GrabDiploma();
        }
    }

    void GrabDiploma()
    {
        if (isHeld) return;

        isHeld = true;
        
        // 1. Move it to the hand
        transform.SetParent(handAnchor);
        
        // 2. Reset position and rotation so it fits the hand
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // 3. Disable physics so it doesn't fall or fly away
        if (GetComponent<Rigidbody>()) 
            GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("Diploma Secured. It is now your voice.");
        
        // TRIGGER NEXT STORY PHASE
        // You can call a function here to change the sky to Murakami Pink!
    }
}
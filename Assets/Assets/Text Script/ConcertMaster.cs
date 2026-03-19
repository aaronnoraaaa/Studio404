using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

public class ConcertMaster : MonoBehaviour
{
    [Header("Effects")]
    public ParticleSystem confetti;      
    public GameObject skyFireworkPrefab; 
    public Renderer diplomaRenderer;
    public TrailRenderer glowTrail;      

    [Header("Glow Settings")]
    [ColorUsage(true, true)] public Color flashColor = Color.yellow * 4f;
    public float glowDuration = 3.0f; 
    private Color idleColor = Color.black;

    private Transform playerCamera;

    void Start()
    {
        // Automatically find the Main Camera (your headset/view)
        if (Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Pointer.current == null || Keyboard.current == null) return;

        // 1. TIP BLAST (Left Click)
        if (Pointer.current.press.wasPressedThisFrame)
        {
            if(confetti != null) confetti.Play();
        }

        // 2. LIGHTSTICK MODE (G Key)
        if (Keyboard.current.gKey.wasPressedThisFrame) 
        {
            StopAllCoroutines();
            StartCoroutine(TemporaryGlow());
        }

        // 3. CAMERA-FACING FIREWORK (F Key)
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            SpawnInFrontOfCamera();
        }
    }

   void SpawnInFrontOfCamera()
    {
        if (skyFireworkPrefab == null || playerCamera == null) return;

        // 1. Get the direction the CAMERA is facing
        Vector3 forwardDir = playerCamera.forward;
        forwardDir.y = 0; 
        forwardDir.Normalize();

        // 2. BRING IT CLOSER: Changed 100f to 15f 
        // 3. LOWER IT: Changed 2f to 1.5f (at eye level)
        Vector3 spawnPos = playerCamera.position + (forwardDir * 90f) + (Vector3.up * 1.5f);
        
        // 4. TIGHTEN SPREAD: Reduced from 40f to 5f so they stay in your view
        spawnPos += new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 3f), Random.Range(-5f, 5f));

        Instantiate(skyFireworkPrefab, spawnPos, Quaternion.identity);
    }

    IEnumerator TemporaryGlow()
    {
        if (diplomaRenderer != null)
        {
            diplomaRenderer.material.EnableKeyword("_EMISSION"); 
            diplomaRenderer.material.SetColor("_EmissionColor", flashColor);
            if(glowTrail != null) glowTrail.emitting = true;
            
            yield return new WaitForSeconds(glowDuration);
            
            diplomaRenderer.material.SetColor("_EmissionColor", idleColor);
            if(glowTrail != null) glowTrail.emitting = false;
        }
    }
}
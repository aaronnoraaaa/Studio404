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

        // 1. Get the direction the CAMERA is facing, but flatten it to the horizon
        Vector3 forwardDir = playerCamera.forward;
        forwardDir.y = 0; 
        forwardDir.Normalize();

        // 2. Spawn 300 units in front of where you are LOOKING
        // Lowered height to 2 units so it stays in the city skyline
        Vector3 spawnPos = playerCamera.position + (forwardDir * 100f) + (Vector3.up * 2f);
        
        // 3. Add random spread so they don't overlap
        spawnPos += new Vector3(Random.Range(-40f, 40f), Random.Range(0f, 15f), Random.Range(-40f, 40f));

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
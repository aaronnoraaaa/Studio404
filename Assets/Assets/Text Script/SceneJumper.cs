using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper : MonoBehaviour
{
    // These functions need to be "public" so the Unity buttons can see them.
    
    public void LoadIntro() { SceneManager.LoadScene("Intro"); }
    
    public void LoadOnboarding() { SceneManager.LoadScene("Onboarding"); }
    
    public void LoadMainConcert() { SceneManager.LoadScene("Main Concert"); }
    
    public void LoadGame() { SceneManager.LoadScene("Game-beat saber"); }
    
    public void LoadOffboarding() { SceneManager.LoadScene("Offboarding"); }
}
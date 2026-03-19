using UnityEngine;

public class BearConcertDirector : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    public Animator bearAnimator;

    void Start()
    {
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying) return;

        float t = audioSource.time;

        if      (t >= 240f) bearAnimator.Play("Offensive Idle");
        else if (t >= 225f) bearAnimator.Play("Shuffling");
        else if (t >= 180f) bearAnimator.Play("Swing Dancing");
        else if (t >= 150f) bearAnimator.Play("Breakdance Uprock Var 2");
        else if (t >= 75f)  bearAnimator.Play("Singing");
        else if (t >= 55f)  bearAnimator.Play("Playing Drums");
        else                bearAnimator.Play("Offensive Idle");
    }
}
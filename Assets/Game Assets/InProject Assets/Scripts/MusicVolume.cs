using UnityEngine;

public class MusicVolume : MonoBehaviour
{
    public float targetVolume = 0f;  
    public float fadeTime = 3f;    

    private float initialVolume;  
    private float currentVolume;    
    private float fadeSpeed;        

    private AudioSource audioSource;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialVolume = audioSource.volume;
        currentVolume = initialVolume;
        fadeSpeed = Mathf.Abs(targetVolume - initialVolume) / fadeTime;
    }

    void Update()
    {
        
        if (Mathf.Abs(currentVolume - targetVolume) > 0.01f)
        {
            currentVolume = Mathf.MoveTowards(currentVolume, targetVolume, fadeSpeed * Time.deltaTime);
            audioSource.volume = currentVolume;
        }
    }
}
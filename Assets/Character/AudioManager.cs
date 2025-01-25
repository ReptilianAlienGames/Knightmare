using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] grassFootstepSounds;
    public AudioClip[] waterFootstepSounds;
    public AudioClip[] metalFootstepSounds;
    public AudioClip[] woodFootstepSounds;
    public AudioClip[] sandFootstepSounds;
    public AudioClip[] snowFootstepSounds;

    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstepSound(string surfaceTag, float volume = 1.0f)
    {
        AudioClip[] footstepSounds = GetFootstepSounds(surfaceTag);

        if (footstepSounds != null && footstepSounds.Length > 0)
        {
            AudioClip clipToPlay = footstepSounds[Random.Range(0, footstepSounds.Length)];
            float randomPitch = Random.Range(minPitch, maxPitch);
            audioSource.pitch = randomPitch;
            audioSource.PlayOneShot(clipToPlay, volume);
            // Reset pitch to default after playing the sound
            audioSource.pitch = 1.0f;
        }
    }

    private AudioClip[] GetFootstepSounds(string surfaceTag)
    {
        switch (surfaceTag)
        {
            case "Grass":
                return grassFootstepSounds;
            case "Water":
                return waterFootstepSounds;
            case "Metal":
                return metalFootstepSounds;
            case "Wood":
                return woodFootstepSounds;
            case "Sand":
                return sandFootstepSounds;
            case "Snow":
                return snowFootstepSounds;
            default:
                Debug.LogWarning("Surface tag not recognized: " + surfaceTag);
                return null;
        }
    }
}

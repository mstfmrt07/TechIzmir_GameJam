using UnityEngine;

public class SoundManager : MSingleton<SoundManager>, IGameEventsHandler
{
    [Header("References")]
    public AudioClip bgMusic;
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip fallClip;
    public AudioClip collectClip;
    public AudioClip lightningClip;
    public AudioClip explosionClip;
    public AudioClip flowerPotClip;
    public AudioClip dieClip;
    public AudioClip clickClip;


    private AudioSource bgSource;

    private void Awake()
    {
        SubscribeGameEvents();
        bgSource = PlaySound(bgMusic, true);
        bgSource.volume = 0.5f;
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnGameLoad += OnGameLoad;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameFailed += OnGameFailed;
        GameEvents.OnGameRecovered += OnGameRecovered;
    }

    public AudioSource PlaySound(AudioClip clip, bool looping = false, float lifeTime = -1f)
    {
        if (!SaveManager.Instance.SoundOn)
            return null;

        GameObject soundGameObject = new GameObject(clip.name);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;

        if (looping)
        {
            audioSource.loop = true;
            audioSource.Play();

            if (lifeTime > 0f)
            {
                Destroy(soundGameObject, lifeTime);
            }
        }
        else
        {
            audioSource.PlayOneShot(clip);
            Destroy(soundGameObject, clip.length);
        }

        return audioSource;
    }

    public void OnGameLoad()
    {
        bgSource.Play();
    }

    public void OnGameStarted()
    {
    }

    public void OnGameFailed()
    {
        bgSource.Pause();
    }

    public void OnGameRecovered()
    {
        bgSource.Play();
    }
}

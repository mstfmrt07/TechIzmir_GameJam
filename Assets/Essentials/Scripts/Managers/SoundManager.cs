using UnityEngine;

public class SoundManager : MSingleton<SoundManager>, IGameEventsHandler
{
    [Header("References")]
    public AudioClip bgMusic;
    public AudioClip buttonClick;
    public AudioClip playCard;
    public AudioClip inspectCard;
    public AudioClip hit;
    public AudioClip bossHit;
    public AudioClip initDeck;
    public AudioClip success;
    public AudioClip fail;


    private AudioSource bgSource;

    private void Awake()
    {
        SubscribeGameEvents();
        //bgSource = PlaySound(bgMusic, true);
        //bgSource.volume = 0.5f;
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnLevelLoaded += OnLevelLoaded;
        GameEvents.OnLevelStarted += OnLevelStarted;
        GameEvents.OnLevelFailed += OnLevelFailed;
        GameEvents.OnLevelSucceeded += OnLevelSucceeded;
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

    public void OnLevelLoaded()
    {
    }

    public void OnLevelStarted()
    {
    }

    public void OnLevelFailed()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.fail);
    }

    public void OnLevelSucceeded()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.success);
    }
}

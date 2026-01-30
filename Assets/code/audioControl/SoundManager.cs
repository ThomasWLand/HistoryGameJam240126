using UnityEngine;

public enum GameSounds
{
    AMBIENT_SEA,
    SHOT_CRITICAL,
    SHOT_HIT,
    SHOT_MISS,
    UI_HOVER,
    UI_PRESS
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] AudioSource ambientSound;
    [SerializeField] AudioSource _soundEmitter;

    public AudioClip[] shotHit;
    public AudioClip[] shotMiss;
    public AudioClip[] shotCritical;
    public AudioClip[] uiHover;
    public AudioClip[] uiPress;

    void Awake()
    {
        if(instance != null)
        {
            if(instance != this)
            {
                Destroy(this);
            }
        }
        else
        {
            instance = this;
        }
    }

    public static void PlaySound(GameSounds sound)
    {
        instance.PlayGameSound(sound);
    }

    public void PlayGameSound(GameSounds sound)
    {
        AudioClip clip = this._getSound(sound);
        this._soundEmitter.clip = clip;
        this._soundEmitter.Play();
    }

    public void PlayGameSound(int index)
    {
        GameSounds sound = (GameSounds)index;
        AudioClip clip = this._getSound(sound);
        this._soundEmitter.clip = clip;
        this._soundEmitter.Play();
    }

    private AudioClip _getSound(GameSounds sound)
    {
        AudioClip[] sources;
        switch(sound)
        {
            case GameSounds.SHOT_CRITICAL:
                sources = shotCritical;
                break;
            case GameSounds.SHOT_HIT:
                sources = shotHit;
                break;
            case GameSounds.SHOT_MISS:
                sources = shotMiss;
                break;
            case GameSounds.UI_HOVER: 
                sources = uiHover;
                break;
            case GameSounds.UI_PRESS:
                sources = uiPress;
                break;
            default:
                Debug.LogError("error: tried playing " + sound.ToString() + " but no sound configured");
                return null;
        }
        int randIndex = Random.Range(0, sources.Length - 1);

        return sources[randIndex];
    }
}

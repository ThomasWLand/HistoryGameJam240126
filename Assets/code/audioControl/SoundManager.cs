using UnityEngine;
using UnityEngine.Rendering;

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

    private AudioSource[] sources;
    private int sourceCount = 16, currIndex = 0;

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

        sources = new AudioSource[sourceCount];
        currIndex = 0;
        for(int i = 0; i < sourceCount; i++)
        {
            GameObject newSource = new GameObject();
            newSource.transform.parent = this.transform;
            newSource.name = "audioChannel" + i;
            AudioSource component = newSource.AddComponent<AudioSource>();
            component.playOnAwake = false;
            sources[i] = component;
        }
    }

    private void Start()
    {
        //for now lets default the volume to 0.5
        ambientSound.volume = 0.5f;
        foreach (AudioSource source in sources) 
        {
            source.volume = 0.5f;
        }
    }

    public void SetVolume(float volume)
    {
        ambientSound.volume = volume;
        foreach (AudioSource source in sources) 
        {
            source.volume = volume;
        }
    }

    public static void PlaySound(GameSounds sound)
    {
        instance.PlayGameSound(sound);
    }

    public void PlayGameSound(GameSounds sound)
    {
        AudioSource source = sources[currIndex];
        AudioClip clip = this._getSound(sound);
        source.clip = clip;
        source.Play();
        currIndex = currIndex >= sourceCount - 1 ? 0 : currIndex + 1;
    }

    public void PlayGameSound(int index)
    {
        GameSounds sound = (GameSounds)index;
        this.PlayGameSound(sound);
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

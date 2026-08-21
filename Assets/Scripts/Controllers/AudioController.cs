using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private float m_audioVolume = 1;

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioSource m_stoppableAudioSource;

    public float AudioVolume => m_audioVolume;

    public static AudioController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        AudioHolder.OnAudio += PlayAudio;
        AudioHolder.OnStoppableAudio += PlayStoppableAudio;
    }

    private void OnDisable()
    {
        AudioHolder.OnAudio -= PlayAudio;
        AudioHolder.OnStoppableAudio -= PlayStoppableAudio;
    }
    
    private void Start()
    {
        m_audioVolume = PlayerPrefs.GetFloat(PrefKeys.Volume, 1f);
    }

    public void PlayAudio(AudioData data)
    {
        float pitch = data.Pitch;
        if (data.IsPitchRandom) pitch = Random.Range(data.Min, data.Max);
        m_audioSource.pitch = pitch;
        m_audioSource.PlayOneShot(data.Clip, m_audioVolume);
    }

    public void PlayStoppableAudio(AudioData data)
    {
        float pitch = data.Pitch;
        if (data.IsPitchRandom) pitch = Random.Range(data.Min, data.Max);
        m_stoppableAudioSource.pitch = pitch;
        m_stoppableAudioSource.Stop();
        m_stoppableAudioSource.PlayOneShot(data.Clip, m_audioVolume);
    }

    public void SetAudio(float value)
    {
        m_audioVolume = value;
    }
}
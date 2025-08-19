using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip matchSound;   // Âm thanh khi ghép thành công
    public AudioClip clickSound;   // Âm thanh khi nhấn ô
    public AudioClip bgMusic;      // Nhạc nền
    public AudioClip errorSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ âm nhạc khi đổi scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMusic(bgMusic);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
}

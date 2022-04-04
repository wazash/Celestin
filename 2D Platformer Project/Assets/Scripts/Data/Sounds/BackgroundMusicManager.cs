using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private FloatReference duration;

    [SerializeField] private BackgroundMusic bgMusic;

    private void OnEnable()
    {
        EventsManager.OnMusicFade.AddListener(FadeMusic);
    }
    private void OnDisable()
    {
        EventsManager.OnMusicFade.RemoveListener(FadeMusic);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = 0;
    }

    private void Update()
    {
        BackgroundMusicChanger();
    }

    private void BackgroundMusicChanger()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && audioSource.clip != bgMusic.MainMenuClip)
        {
            audioSource.clip = bgMusic.MainMenuClip;
            EventsManager.OnMusicFade.Invoke(1, duration.Value);
            audioSource.Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && audioSource.clip != bgMusic.ForestLevelClip)
        {
            audioSource.clip = bgMusic.ForestLevelClip;
            EventsManager.OnMusicFade.Invoke(1, duration.Value);
            audioSource.Play();
        }
    }

    private void FadeMusic(float targetValue, float duration)
    {
        audioSource.DOFade(targetValue, duration);
    }

    [System.Serializable]
    public struct BackgroundMusic
    {
        public AudioClip MainMenuClip;
        public AudioClip ForestLevelClip;
    }
}

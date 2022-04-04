using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource playerAudioSource;

    [SerializeField] private float footstepsPlayOffset = 0.3f;
    private float lastTimePlayed = 0;

    [SerializeField] private Vector2 pitchValue;

    private void OnEnable()
    {
        EventsManager.OnPlayedSfxPlay.AddListener(PlayPlayerSFX);
    }
    private void OnDisable()
    {
        EventsManager.OnPlayedSfxPlay.RemoveListener(PlayPlayerSFX);
    }

    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerSFX(AudioClip clip)
    {
        playerAudioSource.pitch = Random.Range(pitchValue.x, pitchValue.y);

        if(clip.name != "sfx_gravel_player_footstep")
        {
            playerAudioSource.PlayOneShot(clip);
        }
        else
        {
            //lastTimePlayed = Time.time;
            if(lastTimePlayed + footstepsPlayOffset < Time.time)
            {
                playerAudioSource.PlayOneShot(clip);
                lastTimePlayed = Time.time;
            }
        }
    }
}
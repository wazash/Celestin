using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenCover : MonoBehaviour
{
    private Image image;
    public FloatReference fadingDuration;
    public FloatReference startDelay;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
    }
    void Start()
    {
        EventsManager.OnMusicFade.Invoke(1, fadingDuration.Value + startDelay.Value);
        // Make screen cover transparent, then invoke event enabling controll character
        image.DOFade(0, fadingDuration.Value).SetDelay(startDelay.Value).OnComplete(() =>
        {
            EventsManager.OnPlayerControllPossibilityChanged?.Invoke(true);
        });
    }
}

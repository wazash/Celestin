using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShowMainMenuButtons : MonoBehaviour
{
    [SerializeField] private float endPositionX;

    [SerializeField] private FloatReference fadeCoverDuration;
    [SerializeField] private FloatReference fadeCoverDelay;
    [SerializeField] private FloatReference duration;
    private float delay;

    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        // Calculate delay based on startinf fade duration
        delay = fadeCoverDelay.Value + fadeCoverDuration.Value;

        // Push buttons one by one
        buttons[0].GetComponent<RectTransform>().DOAnchorPosX(endPositionX, duration.Value).SetEase(Ease.OutBack).SetDelay(delay).OnComplete(() =>
        {
            buttons[1].GetComponent<RectTransform>().DOAnchorPosX(endPositionX, duration.Value).SetEase(Ease.OutBack).OnComplete(() =>
            {
                buttons[2].GetComponent<RectTransform>().DOAnchorPosX(endPositionX, duration.Value).SetEase(Ease.OutBack);
            });
        });
    }
}

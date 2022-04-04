using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuLayoutsManager : MonoBehaviour
{
    [SerializeField] private FloatReference offPosition;
    [SerializeField] private FloatReference onPosition;

    private RectTransform rectTransform;

    [SerializeField] private float duration;

    [SerializeField] private GameObject goToSelect;

    private static int clicksCounter;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Set butons active at the start
        SetButtonsStatus(false);
    }

    public void ShowLayout(GameObject goToSelect)
    {
        // Set butons active
        SetButtonsStatus(true);
        EventsManager.OnSelectedMenuItem.Invoke(goToSelect);

        if(clicksCounter < 1)
        {
            // If buttons were not clicked, push menu layout without delay
            rectTransform.DOAnchorPosX(onPosition.Value, duration).SetEase(Ease.OutBack);   
        }
        else
        {
            // Set delay if is not firt button click
            rectTransform.DOAnchorPosX(onPosition.Value, duration).SetDelay(duration).SetEase(Ease.OutBack);
        }

        clicksCounter++;
    }

    public void HideLayout()
    {
        // Set buttons inactive
        SetButtonsStatus(false);

        // Hide layout
        rectTransform.DOAnchorPosX(offPosition.Value, duration).SetEase(Ease.InBack);   
    }

    private void SetButtonsStatus(bool value)
    {
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            button.enabled = value;
        }
    }
}
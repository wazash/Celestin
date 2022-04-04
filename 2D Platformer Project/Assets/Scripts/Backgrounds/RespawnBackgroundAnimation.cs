using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RespawnBackgroundAnimation : MonoBehaviour
{
    [SerializeField]
    private FloatReference backgroundAnimationTime;
    private Image respawnBG;

    [SerializeField]
    private AnimType animType;

    private void Start()
    {
        respawnBG = GetComponent<Image>();
        respawnBG.fillAmount = 0;
    }

    private void OnEnable()
    {
        EventsManager.OnBGRespawn.AddListener(PlayerDeathAnimationBG);
    }

    private void OnDisable()
    {
        EventsManager.OnBGRespawn.RemoveListener(PlayerDeathAnimationBG);
    }

    public void PlayerDeathAnimationBG()
    {
        switch (animType)
        {
            case AnimType.LeftToRightThenRightToLeft:
                LeftToRightThenRightToLeft();
                break;
            case AnimType.LeftToRightThenLeftToRight:
                LeftToRightThenLeftToRight();
                break;
        }

    }
    private void LeftToRightThenRightToLeft()
    {
        respawnBG.fillMethod = Image.FillMethod.Horizontal;
        respawnBG.DOFillAmount(1, backgroundAnimationTime.Value).SetLoops(2, LoopType.Yoyo);
    }

    private void LeftToRightThenLeftToRight()
    {
        respawnBG.fillMethod = Image.FillMethod.Horizontal;
        respawnBG.DOFillAmount(1, backgroundAnimationTime.Value).OnComplete(() =>
        {
            respawnBG.fillOrigin = (int)Image.OriginHorizontal.Right;
            respawnBG.DOFillAmount(0, backgroundAnimationTime.Value).OnComplete(() =>
            {
                respawnBG.fillOrigin = (int)Image.OriginHorizontal.Left;
            });
        });
    }
}

public enum AnimType
{
    LeftToRightThenRightToLeft,
    LeftToRightThenLeftToRight
}

using DG.Tweening;
using UnityEngine;

public class KeyFloatAnimation : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float valueY;
    [SerializeField]
    private float cycleLength = 2f;

    private Tween tween;

    // Start is called before the first frame update
    private void Start()
    {
        tween = transform.DOMoveY(transform.position.y + valueY, cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        tween.Kill();
    }
}

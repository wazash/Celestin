using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class VerticalDoors : MonoBehaviour
{
    [SerializeField]
    private int id;

    [Header("Shake")]
    [SerializeField, Range(0.0f, 5.0f)]
    private float shakeDuration = 1f;
    [SerializeField, Range(0.0f, 0.1f)]
    private float shakeStrength = 0.05f;
    [SerializeField, Range(0, 100)]
    private int shakeVibratio = 20;

    [Header("Move")]
    [SerializeField, Range(0.0f, 10.0f)]
    private float verticalOffset;
    [SerializeField, Range(0.0f, 5.0f)]
    private float duration;
    private Transform doorSpriteTransform;

    [Header("Virtual Camera")]
    [SerializeField]
    private bool shouldCameraMove;
    private CinemachineVirtualCamera vCamera;

    public static event Action<CinemachineVirtualCamera, int> OnPriorityCameraChaged;

    private void Start()
    {
        doorSpriteTransform = GetComponentInChildren<Collider2D>().transform;
        vCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        KeyBehaviour.OnKeyCollected += Open;
    }
    private void OnDisable()
    {
        KeyBehaviour.OnKeyCollected -= Open;
    }

    private void Open(int value)
    {
        if(value == id)
        {
            MoveDoor(verticalOffset, duration);
        }
    }

    private void MoveDoor(float moveOffset, float moveDuration)
    {
        if (shouldCameraMove)
        {
            OnPriorityCameraChaged?.Invoke(vCamera, 11);
        }
        doorSpriteTransform.DOShakePosition(shakeDuration, strength: shakeStrength, vibrato: shakeVibratio).OnComplete(() =>
        {
            doorSpriteTransform.DOMoveY(transform.position.y + moveOffset, moveDuration).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                if (shouldCameraMove)
                {
                    OnPriorityCameraChaged?.Invoke(vCamera, 9);
                }
            });
        });
    }
}

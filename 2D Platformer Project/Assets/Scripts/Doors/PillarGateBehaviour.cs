using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;

public class PillarGateBehaviour : MonoBehaviour
{
    [Header("Doors Info")]
    [SerializeField]
    private int DoorsID;
    [SerializeField]
    private Vector3 newDoorPosition;
    [SerializeField]
    private bool shouldCameraMove;

    [Header("Animation")]
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private float shakeDuration = 1f;
    private float shakeStrength = 0.05f;
    private int shakeVibratio = 20;

    private Transform gateVisualTransform;
    [SerializeField]private CinemachineVirtualCamera doorsVirtualCamera;

    private void Start()
    {
        // Get components
        doorsVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        gateVisualTransform = GetComponentInChildren<BoxCollider2D>().gameObject.transform;
    }

    // Subscribe and unsubscribe events
    private void OnEnable()
    {
        EventsManager.OnKeyCollected.AddListener(RaisePillarDoors);
    }
    private void OnDisable()
    {
        EventsManager.OnKeyCollected?.RemoveListener(RaisePillarDoors);
    }

    private void RaisePillarDoors(int id)
    {
        if (DoorsID == id)
        {
            EventsManager.OnPillarDoorsStarted.Invoke(); // Default starting event

            if (shouldCameraMove)
            {
                EventsManager.OnCameraPriorityChanged.Invoke(doorsVirtualCamera, 11);   // Make camera move to doors
                EventsManager.OnPlayerControllPossibilityChanged.Invoke(false); // Prevent player movement
            }

            gateVisualTransform.DOShakePosition(shakeDuration, strength: shakeStrength, vibrato: shakeVibratio).OnComplete(() =>
            {
                EventsManager.OnPillarDoorsShakeEnded.Invoke(); // Default shake end event
                gateVisualTransform.DOMove(newDoorPosition, moveDuration).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
               {
                   EventsManager.OnPillarDoorsMoveEnded.Invoke();   // Default move end event

                   if (shouldCameraMove)
                   {
                       EventsManager.OnCameraPriorityChanged.Invoke(doorsVirtualCamera, 9); // Make camera back
                       EventsManager.OnPlayerControllPossibilityChanged.Invoke(true);   // Make player possibility to move
                   }
               });
            }); 
        }
    }
}

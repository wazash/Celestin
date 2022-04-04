using Cinemachine;
using UnityEngine;

public class VirtualCamerasPriorityChanger : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.OnCameraPriorityChanged.AddListener(ChangeCameraPriority);
    }

    private void OnDisable()
    {
        EventsManager.OnCameraPriorityChanged.RemoveListener(ChangeCameraPriority);
    }

    public void ChangeCameraPriority(CinemachineVirtualCamera vCamera, int priorityValue)
    {
        vCamera.Priority = priorityValue;
    }
}

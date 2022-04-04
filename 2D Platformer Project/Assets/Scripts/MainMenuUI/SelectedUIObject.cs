using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedUIObject : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.OnSelectedMenuItem.AddListener(ChangeSelectedItem);
    }
    private void OnDisable()
    {
        EventsManager.OnSelectedMenuItem.RemoveListener(ChangeSelectedItem);
    }

    public void ChangeSelectedItem(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(obj);
    }
}

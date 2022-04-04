using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.OnLoadLevel.AddListener(LoadLevel);
    }
    private void OnDisable()
    {
        EventsManager.OnLoadLevel.RemoveListener(LoadLevel);
    }

    private void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}

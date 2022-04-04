using UnityEngine;

public class GameplayMainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    private void ShowGameplayMainMenu(bool value)
    {
        if (value)
        {
            menuWindow.SetActive(true);
        }
        else
        {
            menuWindow.SetActive(false);
        }
    }
}

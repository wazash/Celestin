using UnityEngine;

public class KillPlayerOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventsManager.OnPlayerDeath.Invoke();
        }
    }
}

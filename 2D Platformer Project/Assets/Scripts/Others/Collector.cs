using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectible = collision.GetComponent<ICollectable>();
        collectible?.Collect();
    }
}

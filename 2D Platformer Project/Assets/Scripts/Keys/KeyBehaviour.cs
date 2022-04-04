using System;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour, ICollectable
{
    public int doorID;

    public static event Action<int> OnKeyCollected;
    public void Collect()
    {
        Destroy(this.gameObject);
        OnKeyCollected?.Invoke(doorID);
    }
}

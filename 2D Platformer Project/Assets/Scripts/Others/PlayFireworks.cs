using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFireworks : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.OnWinObjectCollected.AddListener(StartFireworks);
    }

    private void OnDisable()
    {
        EventsManager.OnWinObjectCollected.RemoveListener(StartFireworks);
    }

    private void StartFireworks()
    {
        GetComponent<ParticleSystem>().Play();
    }
}

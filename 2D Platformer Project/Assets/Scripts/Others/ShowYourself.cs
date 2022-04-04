using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowYourself : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.OnWinObjectCollected.AddListener(TurnOn);
    }

    private void OnDisable()
    {
        EventsManager.OnWinObjectCollected.RemoveListener(TurnOn);
    }

    private void Start()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }

    private void TurnOn()
    {
        GetComponent<TilemapRenderer>().enabled = true;
    }
}

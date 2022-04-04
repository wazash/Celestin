using Cinemachine;
using System;
using UnityEngine;

public static class EventsManager
{
    // Camera events
    public static readonly EventBase<CinemachineVirtualCamera, int> OnCameraPriorityChanged = new EventBase<CinemachineVirtualCamera, int>();

    // Player Events
    public static readonly EventBase<bool> OnPlayerControllPossibilityChanged = new EventBase<bool>();
    public static readonly EventBase OnPlayerDeath = new EventBase();
    public static readonly EventBase OnPlayerRespawn = new EventBase();

    public static readonly EventBase<AudioClip> OnPlayedSfxPlay = new EventBase<AudioClip>();

    // BG events
    public static readonly EventBase OnBGRespawn = new EventBase();

    // Sounds events
    public static readonly EventBase<float, float> OnMusicFade = new EventBase<float, float>();

    // UI events
    public static readonly EventBase<GameObject> OnSelectedMenuItem = new EventBase<GameObject>();
    // Keys events
    public static readonly EventBase<int> OnKeyCollected = new EventBase<int>();

    // Pillars doors events
    public static readonly EventBase OnPillarDoorsStarted = new EventBase();
    public static readonly EventBase OnPillarDoorsShakeEnded = new EventBase();
    public static readonly EventBase OnPillarDoorsMoveEnded = new EventBase();

    // Win events
    public static readonly EventBase OnWinObjectCollected = new EventBase();

    // SceneManger events
    public static readonly EventBase<int> OnLoadLevel = new EventBase<int>();
    public static readonly EventBase OnLevelLoaded = new EventBase();
}
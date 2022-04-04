using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player/Base Data")]
public class PlayerData : ScriptableObject
{
    #region Gravity
    [Header("Gravity")]
    [HideInInspector]
    public float Gravity;
    [HideInInspector]
    public float GroundedGravity = -0.01f;
    #endregion

    #region Moving State
    [Header("Moving")]
    [Tooltip("Maximum player grounded speed")]
    public float MovementVelocity = 5.0f;
    [Tooltip("Acceleration factor. How fast player will accelerate")]
    public float Acceleration = 5f;
    [Tooltip("Deceleration factor. How fast player will decelerate")]
    public float Deceleration = 5f;
    [Tooltip("Power of de/acceleration applying")]
    public float VelocityPower = 0.9f;
    [Range(0f, 0.1f), Tooltip("If player will moving slower, his velocity will be set at 0")]
    public float MinGroundedVelocityX = 0.01f;
    [Range(0f, 0.1f), Tooltip("If player falling moving slower, his velocity will be set at 0")]
    public float MinGroundedVelocityY = 0.01f;
    #endregion

    #region Jumping State
    [Header("Jumping")]
    [Tooltip("How high player can jump by default")]
    public float MaxJumpHeight;
    [HideInInspector, Tooltip("Jump power. Calculate from maximum jump height and time")]
    public float InitialJumpVelocity;
    [Tooltip("How long the jump will last by default")]
    public float MaxJumpTime;
    [Tooltip("Maximum number of jumps")]
    public int AmountOfJumps = 1;
    [Range(0f, 0.5f), Tooltip("Short time after leaving the ground when player can jump")]
    public float CoyoteTime = 0.1f;
    [Tooltip("Maximum falling velocity")]
    public float MaxFaliingSpeed = 10;
    [Range(0f, 1.0f), Tooltip("Factor determining jumping velocity reduction on release jump button")]
    public float JumpVelocityReductionFactor = 0.1f;
    #endregion

    #region Wall Jumping State
    [Header("Wall Juming")]
    [Range(0f, 1f)]
    public float WallJumpVelocityMultiplier = 0.5f;
    public float WallJumpTime = 0.3f;
    public Vector2 WallJumpOffAngle = new Vector2(1, 2);
    public Vector2 WallJumpUpAngle = new Vector2(0.5f, 2f);
    #endregion

    #region Dashing State
    [Header("Dashing")]
    public float BeforeDashFreezeTime = 0.05f;
    public float DashVelocity = 12;
    public int AmountOfDashes = 1;
    public float DashCooldown = 0.5f;
    public float DashTime = 0.3f;
    public float DashDrag = 10f;
    [Range(0.0f, 1.0f)]
    public float DashEndMultiplierY = 0.2f;
    public float DistanceBetweenAfterImages = 0.5f;
    #endregion

    #region Wall States
    [Header("Wall Sliding")]
    [Range(0f, 10f)]
    public float WallSlideVelocity = 2.5f;

    [Header("Wall Climing")]
    public float WallClimbVelocity = 3f;

    [Header("Ledge Climing")]
    public Vector2 StartOffSet;
    public Vector2 StopOffSet;
    #endregion

    #region Chekers
    [Header("Checkers Variables")]
    [Range(0.0f, 1.0f)]
    public float WallCheckDistace = 0.2f;
    [Range(0.0f, 2.0f)]
    public float GroundCheckRadius = 0.3f;
    public float GroundCheckWidth = 0.3f;
    public float GroundCheckHeight = 0.3f;
    public LayerMask WhatIsGround;
    #endregion

    #region SFX
    [Header("Sounds Effects")]
    public PlayerSFX PlayerSounds;

    #endregion

    [Header("Other")]
    public Vector2 CurrentSpawnpointPosition;
}

/// <summary>
/// Player animations names
/// </summary>
public enum AnimationName
{
    Idling,
    Running,
    InAir,
    WallGrab,
    WallSlide,
    WallClimb,
    Ledge
}

[System.Serializable]
public struct PlayerSFX
{
    public AudioClip JumpClip;
    public AudioClip LandClip;
    public AudioClip FootstepsClip;
}
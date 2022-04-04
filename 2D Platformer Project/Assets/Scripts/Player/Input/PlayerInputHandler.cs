using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Create getter 
    private PlayerInput playerInput;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabWallInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool PauseInput { get; private set; }

    [SerializeField, Tooltip("Time, how long input will 'hold' true in jump input value after press jump button")]
    private float inputHoldTime = 0.2f;

    [SerializeField, Tooltip("Using stick causes bug that while wall climbing cannot change direction, this defines " +
        "threshold of value over which stick will be 'flatted' to whole value")]
    private bool useThreshold = true;
    [SerializeField]
    [Range(0f, 1f)]
    private float stickThreshold = .5f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    // Getting movement vector2 value from Input System
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (useThreshold)
        {
            // Normalized 'X' value
            if (Mathf.Abs(RawMovementInput.x) > stickThreshold)
            {
                NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            }
            else
            {
                NormalizedInputX = 0;
            }

            // Normalized 'Y' value
            if (Mathf.Abs(RawMovementInput.y) > stickThreshold)
            {
                NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
            }
            else
            {
                NormalizedInputY = 0;
            }
        }
        else
        {
            NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        
    }

    // Getting jump button value from Input System
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false; 
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    /// <summary>
    /// Make jump input used (set to flase)
    /// </summary>
    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    // Getting grab wall value form Input System
    public void OnGrabWallInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabWallInput = true;
        }
        if (context.canceled)
        {
            GrabWallInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
        }
    }
    public void UseDashInput() => DashInput = false;
    private void CheckDashInputHoldTime()
    {
        if(Time.time >= dashInputStartTime + inputHoldTime)
        {
            UseDashInput();
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseInput = !PauseInput;
        }
    }

}

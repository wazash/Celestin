using UnityEngine;

[System.Serializable]
public struct PlayerParticles
{
    public ParticleSystem LandingPS;
    public ParticleSystem JumpingPS;
    public ParticleSystem DeathPS;
    public ParticleSystem SpawnSP;
}

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }


    [SerializeField]
    private PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } }
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion

    #region Check variables
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    #endregion

    #region Other variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; } = 1; // -1 - left, 1 - right 

    [field: SerializeField] public PlayerParticles Particles { get; private set; }

    public bool IsControllable { get; private set; } = false;

    private Vector2 workspace;
    #endregion


    #region Unity callback functions
    private void Awake()
    {
        // Create State Machine
        StateMachine = new PlayerStateMachine();

        //Create States
        IdleState = new PlayerIdleState(this, StateMachine, playerData, AnimationName.Idling.ToString());
        MoveState = new PlayerMoveState(this, StateMachine, playerData, AnimationName.Running.ToString());
        JumpState = new PlayerJumpState(this, StateMachine, playerData, AnimationName.InAir.ToString());
        InAirState = new PlayerInAirState(this, StateMachine, playerData, AnimationName.InAir.ToString());
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, AnimationName.WallGrab.ToString());
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, AnimationName.WallSlide.ToString());
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, AnimationName.WallClimb.ToString());
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, AnimationName.InAir.ToString());
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, AnimationName.Ledge.ToString());
        DashState = new PlayerDashState(this, StateMachine, playerData, AnimationName.InAir.ToString());    // change animation state
    }

    private void Start()
    {
        // Get components
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();

        // Initialize starting state
        StateMachine.Initialize(IdleState);

        // Set facing direction to right
        FacingDirection = 1;

        SetupJumpVariables();
    }

    private void Update()
    {
        // Calculate Logic
        CurrentVelocity = Rigidbody.velocity;
        if (IsControllable)
        {
            StateMachine.CurrentState.LogicUpdate();
        }
    }

    private void FixedUpdate()
    {
        // Calculate Physics
        if (IsControllable)
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }

    private void OnEnable()
    {
        EventsManager.OnPlayerControllPossibilityChanged.AddListener(SetIsControllable);
    }

    private void OnDisable()
    {
        EventsManager.OnPlayerControllPossibilityChanged.RemoveListener(SetIsControllable);
    }
    #endregion


    #region Set methods

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check methods
    public bool CheckIsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);
        //return Physics2D.OverlapBox(groundCheck.position, new Vector2(playerData.GroundCheckWidth, playerData.GroundCheckHeight), 0, playerData.WhatIsGround);
    }
    public bool CheckIsTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistace, playerData.WhatIsGround);
    }
    public bool CheckIsTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.WallCheckDistace, playerData.WhatIsGround);
    }
    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistace, playerData.WhatIsGround);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other methods
    private void OnDrawGizmos()
    {
        // Draw ground checker
        Gizmos.color = CheckIsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.GroundCheckRadius);
        //Gizmos.DrawWireCube(groundCheck.position, new Vector3(playerData.GroundCheckWidth, playerData.GroundCheckHeight, 1));

        // Draw front wall checker
        Gizmos.color = CheckIsTouchingWall() ? Color.green : Color.red;
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + playerData.WallCheckDistace * FacingDirection, wallCheck.position.y, wallCheck.position.z));

        // Draw back wall checker
        Gizmos.color = CheckIsTouchingWallBack() ? Color.blue : Color.magenta;
        Gizmos.DrawLine(new Vector3(wallCheck.position.x, wallCheck.position.y, wallCheck.position.z),
            new Vector3(wallCheck.position.x + playerData.WallCheckDistace * -FacingDirection, wallCheck.position.y, wallCheck.position.z));

        // Draw Ledge checker
        Gizmos.color = CheckIfTouchingLedge() ? Color.green : Color.red;
        Gizmos.DrawLine(ledgeCheck.position,
            new Vector3(ledgeCheck.position.x + playerData.WallCheckDistace * FacingDirection, ledgeCheck.position.y, ledgeCheck.position.z));
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    /// <summary>
    /// Determine corner ledge position
    /// </summary>
    /// <returns></returns>
    public Vector2 DetermineCornerPosiotion()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.WallCheckDistace, playerData.WhatIsGround);
        float xDistance = xHit.distance;

        workspace.Set(xDistance * FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)workspace, Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.WhatIsGround);
        float yDistance = yHit.distance;

        workspace.Set(wallCheck.position.x + xDistance * FacingDirection, ledgeCheck.position.y - yDistance);
        return workspace;
    }

    /// <summary>
    /// Calculate falling gravity and needed jump velocity based on maximum jump height
    /// </summary>
    public void SetupJumpVariables()
    {
        float timeToApex = playerData.MaxJumpTime / 2;
        playerData.Gravity = (-2 * playerData.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
        playerData.InitialJumpVelocity = (2 * playerData.MaxJumpHeight) / timeToApex;
    }

    public void SetIsControllable(bool value) => IsControllable = value;

    #endregion
}
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    // Inputs
    protected int xInput;
    private bool jumpInput;
    private bool grabWallInput;
    private bool dashInput;
    private Vector2 dashDirectionInput;

    // Checkers
    private bool isGrounded;
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        // Execute checkers
        isGrounded = player.CheckIsGrounded();
        isTouchingWall = player.CheckIsTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();

        // Reset Abilities
        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Get inputs
        GetInputs();

        player.CheckIfShouldFlip(xInput);

        #region CHANGE STATES
        if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState); // Go to Jump state (ability)
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);    // Go to InAir state
        }
        else if (grabWallInput && isTouchingWall)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && dashDirectionInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.DashState);
        }
        #endregion
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        ApplyGroundedGravity(playerData.GroundedGravity);
    }

    // ===== Own Methods =====
    /// <summary>
    /// Smoothly accelerate player
    /// </summary>
    private void GetInputs()
    {
        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;
        grabWallInput = player.InputHandler.GrabWallInput;
        dashInput = player.InputHandler.DashInput;
        dashDirectionInput = player.InputHandler.DashDirectionInput;
    }

    private void ApplyGroundedGravity(float groundedGravity)
    {
        player.Rigidbody.AddForce(groundedGravity * Vector2.up, ForceMode2D.Force);
    }


}

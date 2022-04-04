using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    // Inputs
    protected int xInput;
    protected int yInput;
    protected bool grabWallInput;
    protected bool jumpInput;
    protected bool dashInput;
    
    // Checkers
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
        isTouchingWall = player.CheckIsTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Get inputs
        xInput = player.InputHandler.NormalizedInputX;
        yInput = player.InputHandler.NormalizedInputY;
        grabWallInput = player.InputHandler.GrabWallInput;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;

        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDrection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabWallInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInput != player.FacingDirection && !grabWallInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }  
        else if(isTouchingWall && !isTouchingLedge && (xInput == player.FacingDirection || yInput >= 0))
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if(dashInput && player.DashState.CheckIfCanDash() && player.InputHandler.DashDirectionInput.x == -player.FacingDirection)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private int xInput;
    private int yInput;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        GetInput();

        player.InAirState.StopWallJumpCoyoteTime();
        DetermineWallJumpDrection(isTouchingWall);


        player.InputHandler.UseJumpInput();
        player.JumpState.DecreaseAmountOfJumpsLeft();

        MakeWallJump(playerData.InitialJumpVelocity * playerData.WallJumpVelocityMultiplier, wallJumpDirection);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        GetInput();

        player.CheckIfShouldFlip(xInput);
        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);

        if (EndWallJumpCondition())
        {
            SetAbilityDone();
        }
    }

    #region Own Methods
    private void GetInput()
    {
        xInput = player.InputHandler.NormalizedInputX;
        yInput = player.InputHandler.NormalizedInputY;
    }

    private void MakeWallJump(float velocity, int direction)
    {
        player.SetVelocityZero();
        Vector2 angle;
        if(yInput == 1 && (xInput == 0 || xInput != wallJumpDirection))
        {
            angle = playerData.WallJumpUpAngle;
        }
        else
        {
            angle = playerData.WallJumpOffAngle;
        }

        player.Rigidbody.AddForce(velocity * direction * new Vector2(angle.x, angle.y * Mathf.Sign(direction)).normalized, ForceMode2D.Impulse);
        player.InAirState.SetIsJumping(true);
    }

    /// <summary>
    ///  Calculate wall jump diraction base on if is touching wall
    /// </summary>
    /// <param name="isTouchingWall"></param>
    public void DetermineWallJumpDrection(bool isTouchingWall) => wallJumpDirection = isTouchingWall ? -player.FacingDirection : player.FacingDirection;

    private bool EndWallJumpCondition() => Time.time >= startTime + playerData.WallJumpTime;
    #endregion

}

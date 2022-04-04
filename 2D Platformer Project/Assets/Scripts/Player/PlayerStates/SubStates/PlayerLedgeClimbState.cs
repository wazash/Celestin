using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPosition;

    private Vector2 cornerPosition;

    private Vector2 startPosition;
    private Vector2 stopPosition;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : 
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityZero();
        player.transform.position = detectedPosition;

        cornerPosition = player.DetermineCornerPosiotion();

        startPosition.Set(cornerPosition.x - (player.FacingDirection * playerData.StartOffSet.x), cornerPosition.y - playerData.StartOffSet.y);
        stopPosition.Set(cornerPosition.x + (player.FacingDirection * playerData.StopOffSet.x), cornerPosition.y + playerData.StopOffSet.y);

        player.transform.position = startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        player.transform.position = stopPosition;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityZero();
        player.transform.position = startPosition;

        if (IsClimbingAnimationFinished())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void SetDetectedPosition(Vector2 position) => detectedPosition = position;

    public bool IsClimbingAnimationFinished() => player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && player.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName.Ledge.ToString());
}

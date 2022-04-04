using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    // ===== Overrides =====

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Change state to MoveState
        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            } 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Decelerate();

        LimitGroundedSpeed(playerData.MinGroundedVelocityX);
    }

    // ===== Own Methods =====
    /// <summary>
    /// Smoothly decelerate player
    /// </summary>
    private void Decelerate()
    {
        float targetSpeed = xInput;

        float speedDifference = targetSpeed - player.CurrentVelocity.x;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * playerData.Deceleration, playerData.VelocityPower) * Mathf.Sign(speedDifference);

        player.Rigidbody.AddForce(movement * Vector2.right);
    }

    /// <summary>
    /// Set X velocity to 0 if current velocity is smaller then given
    /// </summary>
    /// <param name="minimumSpeed"></param>
    private void LimitGroundedSpeed(float minimumSpeed)
    {
        if (Mathf.Abs(player.CurrentVelocity.x) <= minimumSpeed)
        {
            player.SetVelocityX(0);
        }
    }
}

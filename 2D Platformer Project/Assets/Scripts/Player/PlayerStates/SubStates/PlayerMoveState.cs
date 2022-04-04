using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    // ===== Overrides =====
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        EventsManager.OnPlayedSfxPlay.Invoke(playerData.PlayerSounds.FootstepsClip);

        // Change state to IdleState
        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Move();
    }

    // ===== Own Methods =====
    private void Move()
    {
        float targetSpeed = playerData.MovementVelocity * xInput;

        float speedDifference = targetSpeed - player.CurrentVelocity.x;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * playerData.Acceleration, playerData.VelocityPower) * Mathf.Sign(speedDifference);

        player.Rigidbody.AddForce(movement * Vector2.right);
    }
}
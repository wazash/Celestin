using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float startDash;
    private float lastDashTime;

    private bool canDash;
    private bool isDashing;
    private bool isFreezing;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;

    private int frames;
    private int framesF;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) :
        base(player, stateMachine, playerData, animationBoolName)
    {
    }

    #region Override Methods
    public override void Enter()
    {
        base.Enter();

        frames = 0;
        framesF = 0;

        player.SetVelocityZero();

        canDash = false;

        startDash = startTime + playerData.BeforeDashFreezeTime;

        player.InputHandler.UseDashInput();
        player.JumpState.DecreaseAmountOfJumpsLeft();

        isFreezing = true;
        isDashing = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        frames++;



    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        framesF++;

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));

        if (isFreezing)
        {
            DetermineDashDirection();

            if (Time.time >= startTime + playerData.BeforeDashFreezeTime)
            {
                isFreezing = false;
                isDashing = true;
            }
        }

        if (isDashing)
        {
            Dash();
        }

        if (framesF >= 14)
        {
            isDashing = false;
            ApplyEndDashGravity();
            lastDashTime = Time.time;
            SetAbilityDone();
        }
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log(frames + " | " + framesF);
    }
    #endregion

    #region Own Methods
    private void Dash()
    {
        //player.SetVelocityY(playerData.DashVelocity);
        player.Rigidbody.AddForce(playerData.DashVelocity * dashDirection, ForceMode2D.Impulse);
    }
    private void DetermineDashDirection()
    {
        dashDirectionInput = player.InputHandler.DashDirectionInput;
        if (dashDirectionInput != Vector2.zero)
        {
            dashDirection = dashDirectionInput.normalized;
        }
    }
    private void ApplyEndDashGravity()
    {
        if (player.CurrentVelocity.y > 0)
        {
            //player.SetVelocityY(player.CurrentVelocity.y * playerData.DashEndMultiplierY);
            player.Rigidbody.AddForce(player.CurrentVelocity.y * playerData.DashEndMultiplierY * Vector2.down, ForceMode2D.Impulse);
        }
    }
    public bool CheckIfCanDash() => canDash && Time.time > lastDashTime + playerData.DashCooldown;
    public void ResetCanDash() => canDash = true;
    #endregion
}

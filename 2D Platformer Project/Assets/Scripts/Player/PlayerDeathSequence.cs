using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSequence : MonoBehaviour
{
    [SerializeField] private float particleDurationOffset;
    [SerializeField] private float bgAnimationDurationOffset;
    [SerializeField] private FloatReference bgAnimationTime;

    private Player player;

    #region Unity Callbacks
    private void OnEnable()
    {
        EventsManager.OnPlayerDeath.AddListener(PlayerDeath);
    }

    private void OnDisable()
    {
        EventsManager.OnPlayerDeath.RemoveListener(PlayerDeath);
    }
    private void Start()
    {
        player = GetComponent<Player>();
    } 
    #endregion

    public void PlayerDeath()
    {
        StartCoroutine(PlayerDeathSeq());
    }

    private IEnumerator PlayerDeathSeq()
    {
        player.SetIsControllable(false);
        player.SetVelocityZero();

        transform.DOScale(0.0f, 0.25f).OnComplete(() => player.Particles.DeathPS.Play());
        yield return new WaitForSecondsRealtime(player.Particles.DeathPS.main.duration + particleDurationOffset);

        // play bg animation
        EventsManager.OnBGRespawn.Invoke();
        yield return new WaitForSecondsRealtime(bgAnimationTime.Value + bgAnimationDurationOffset);

        // respawn player
        EventsManager.OnPlayerRespawn.Invoke();

    }
}

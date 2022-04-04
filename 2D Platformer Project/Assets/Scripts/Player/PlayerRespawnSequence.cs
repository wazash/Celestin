using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerRespawnSequence : MonoBehaviour
{
    [SerializeField] private float particleDurationOffset;
    private Player player;

    #region Unity Callbacks
    private void OnEnable()
    {
        EventsManager.OnPlayerRespawn.AddListener(Respawn);
    }

    private void OnDisable()
    {
        EventsManager.OnPlayerRespawn.RemoveListener(Respawn);
    }

    private void Start()
    {
        player = GetComponent<Player>();
    } 
    #endregion

    private void Respawn()
    {
        StartCoroutine(RespawnSeq());
    }

    private IEnumerator RespawnSeq()
    {
        transform.position = player.PlayerData.CurrentSpawnpointPosition;

        player.Particles.SpawnSP.Play();
        yield return new WaitForSeconds(player.Particles.SpawnSP.main.duration + particleDurationOffset);

        // start player
        transform.DOScale(1.0f, 0.25f).OnComplete(() => player.SetIsControllable(true));
    }
}

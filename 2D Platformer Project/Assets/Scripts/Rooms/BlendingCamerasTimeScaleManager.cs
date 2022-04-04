using Cinemachine;
using UnityEngine;

public class BlendingCamerasTimeScaleManager : MonoBehaviour
{
    private CinemachineBrain brainCM;

    private void Start()
    {
        brainCM = GetComponent<CinemachineBrain>();
    }
    private void Update()
    {
        if (brainCM.IsBlending)
        {
            Time.timeScale = .1f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

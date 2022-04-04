using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Tooltip("Set in doors object to the same as here to connect this key with that doors ")]
    public int DoorsToOpenID;
    
    [Tooltip("If pick key will effect disable player movement, recommended set true")]
    public bool SetPlayerPositionToKeyPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            // Execute order 66 (player picked up key)
            EventsManager.OnKeyCollected.Invoke(DoorsToOpenID);

            // play sound
            // play particle
            // add points

            if (SetPlayerPositionToKeyPosition)
            {
                collision.transform.position = transform.position;
                collision.GetComponent<Player>().SetVelocityZero();
            }
        }
    }

}
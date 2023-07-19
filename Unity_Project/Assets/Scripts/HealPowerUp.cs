using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : MonoBehaviour
{

    public int healthPoints;

    public AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //play the sound : 
            AudioManager.instance.playClipAt(pickupSound, transform.position);

            //give health back to player : 
            PlayerHealth.instance.HealPlayer(healthPoints);
            Destroy(gameObject);
        }
    }
}

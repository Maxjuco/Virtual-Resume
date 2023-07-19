using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //play the sound associated : 
            AudioManager.instance.playClipAt(sound, transform.position);

            //other alternative :
            //AudioSource.PlayClipAtPoint(sound, transform.position);
            //problem = if we want to keep the control on the volume we can't use this...

            //to acess the static instance of inventory : 
            Inventory.instance.AddCoins(1);

            //the scene manager register the coins picked up : 
            CurrentSceneManager.instance.coinsPickedUpInThisScene++;
            Destroy(gameObject);
        }
    }

}

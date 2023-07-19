using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public  GameObject objectToDestroy;

    public AudioClip killSound;
    /*when something enter the area :*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if it is the Player which enter the area : (for that you have to check the tag in the checkbox on the Player GameObject) 
        if (collision.CompareTag("Player"))
        {
            //play the sound of death : 
            AudioManager.instance.playClipAt(killSound, transform.position);

            //hear we delete the instance not only of the weakSpot but of the entier ennemi (graphics + waypoint ....)
            Destroy(objectToDestroy);
            
        }
    }

}

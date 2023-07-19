using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurrentSceneManager.instance.respawnPoint = this.transform.position;

            //if returnig to a checkpoint don't switch the checkpoint..
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

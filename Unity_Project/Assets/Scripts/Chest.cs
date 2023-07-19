using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    private Text interactUI;
    private bool isInRange;

    public Animator animator;

    public int coinsToAdd;

    public AudioClip pickUpSound;

    private void Awake()
    {
        //catch the text component which display the "Press E to interact..." : 
        interactUI = GameObject.FindGameObjectsWithTag("InteractUI")[0].GetComponent<Text>();
        isInRange = false;
    }


    void Update()
    {
        //check the input key : 
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        //to launch the animation
        animator.SetTrigger("OpenChest");

        //then give coins to the player :
        Inventory.instance.AddCoins(coinsToAdd);
        AudioManager.instance.playClipAt(pickUpSound, transform.position);

        //finally to avoid the bug exploit : desactivate the collider : 
        GetComponent<Collider2D>().enabled = false;
        //don't forget to ereased the message in the UI
        interactUI.enabled = false;
        isInRange = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}

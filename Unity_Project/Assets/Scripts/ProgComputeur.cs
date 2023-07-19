using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgComputeur : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public Animator animator;

    public string animationTrigger;

    public AudioClip pickUpSound;

    private void Awake()
    {
        //catch the text component which display the "Press E to interact..." : 
        for(int i = 0; i< GameObject.FindGameObjectsWithTag("InteractUI").Length; i++)
        {
            interactUI = GameObject.FindGameObjectsWithTag("InteractUI")[i].GetComponent<Text>();
        }
       
        isInRange = false;
    }


    void Update()
    {
        //check the input key : 
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            DisplayProg();
        }
    }

    void DisplayProg()
    {
        //to launch the animation
        animator.SetTrigger(animationTrigger);



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

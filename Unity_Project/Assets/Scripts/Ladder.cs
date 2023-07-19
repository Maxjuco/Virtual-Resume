using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{

    private bool isInRange;
    private MovePlayer playerMovement;
    public BoxCollider2D groundUp;

    private Text interactUI;



    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<MovePlayer>();
        //catch the text component which display the "Press E to interact..." : 
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("InteractUI").Length; i++)
        {
            interactUI = GameObject.FindGameObjectsWithTag("InteractUI")[i].GetComponent<Text>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        //to let go the ladder 
        if (isInRange && playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = false;
            groundUp.isTrigger = false;
            //to skip the rest of the code otherwise the climb wont be possible because of the following code ... 
            return;
        }


       

        //to grab the ladder :
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            groundUp.isTrigger = true;
        }

    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMovement.isClimbing = false;
            groundUp.isTrigger = false;

            interactUI.enabled = false;
        }
    }
}

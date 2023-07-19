using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel : MonoBehaviour
{
    private Text interactUI;

    public string TextName;
  

    private void Awake()
    {
        //catch the text component which display the "Press E to interact..." : .
        //look for the right name the table of possible elements :
        for(int i =0; i< GameObject.FindGameObjectsWithTag("InteractUI").Length; i++)
        {
            if(GameObject.FindGameObjectsWithTag("InteractUI")[i].name == TextName)
            {
                interactUI = GameObject.FindGameObjectsWithTag("InteractUI")[i].GetComponent<Text>();
            }
        }
        
       
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            
        }
    }
}

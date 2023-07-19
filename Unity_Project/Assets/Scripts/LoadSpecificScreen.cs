using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScreen : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectsWithTag("FadeSystem")[0].GetComponent<Animator>();
    }

    //when going on the door the game load the level 2 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //make the player pop out : 
            GameObject.FindGameObjectsWithTag("Player")[0].SetActive(false);

            StartCoroutine(loadNextScene());
           
            
        }
    }

    public IEnumerator loadNextScene()
    {

        
        //before loading we trigger the fade in : 
        //we had to put a delay with a co routine to not load before the fade in: 
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);

        //we load the next scene : 
        SceneManager.LoadScene(sceneName);

        /*N.B. : if the fade in do not suffise to hide the player teleportation you have to extend the animation of fade in / fade out ...*/
    }
}

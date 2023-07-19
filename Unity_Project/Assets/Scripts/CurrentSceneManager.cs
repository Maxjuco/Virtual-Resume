using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{


    public int coinsPickedUpInThisScene;

    public Vector3 respawnPoint;

    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Two instance of CurrentSceneManager are in the scene !");
            return;
        }

        instance = this;

        //set the default respawn point on the player location
        respawnPoint = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
    }

    
}

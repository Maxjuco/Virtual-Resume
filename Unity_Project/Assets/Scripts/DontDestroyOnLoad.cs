using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] objects;
    void Awake()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(1366, 768, Screen.fullScreen);

        
        
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }

        if (GameObject.FindGameObjectsWithTag("audio").Length != 1)
            Destroy(GameObject.FindGameObjectsWithTag("audio")[0]);



    }



}

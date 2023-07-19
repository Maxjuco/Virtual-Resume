using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Two instance of GameOverManager are in the scene !");
            return;
        }

        instance = this;
    }


    public void OnPlayerDeath()
    {        
        //display the game over menu :
        gameOverUI.SetActive(true);
    }


    public void RetryButton()
    {
        //start the level again : 

        //reset number of coins obtained in the level : 
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisScene);

        //reload the scene completly (if you want to reset the ennemies, coins, etc ... )
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//re load the actual scene 

        //to reset player health movement and all of that : 
        PlayerHealth.instance.Respawn();


        //don't forget to re hide the Game over menu : 
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {       
        //lead to the main menu ... 
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitButton()
    {
        //close the game
        Application.Quit();
    } 
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //to know if the game is on pause : 
    public static bool gameisPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingWindow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameisPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }


    void Paused()
    {
        //desactivating the possibility of using input for the player movement :
        MovePlayer.instance.enabled = false;
        //activating the Pause menu : 
        pauseMenuUI.SetActive(true);
        //stoping the time in the game 
        Time.timeScale = 0; //Time.timeScale = portion of time passed  (if zero time freeze)
        //change the game statut...
        gameisPaused = true;
    }

    public void Resume()
    {
        //desactivating the Pause menu : 
        pauseMenuUI.SetActive(false);
        settingWindow.SetActive(false);
        //reactivating the possibility of using input for the player movement :
        MovePlayer.instance.enabled = true;
        //resuming the time in the game 
        Time.timeScale = 1; //Time.timeScale = portion of time passed  (if 0 time freeze, if 1 = normal time)
        //change the game statut...
        gameisPaused = false;
    }


    public void OpenSettings()
    {
        settingWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        settingWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
       

        //to avoid freeze on the next game : 
        Resume();

        //to load the main menu 
        SceneManager.LoadScene("MainMenu");
    }
}

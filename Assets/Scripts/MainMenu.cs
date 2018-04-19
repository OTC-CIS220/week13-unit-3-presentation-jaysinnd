using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //when you push the Play button, it calls the next scene in line. 
    }

    public void QuitGame()
    {
        Application.Quit(); //this calls the application to quit
    }
}

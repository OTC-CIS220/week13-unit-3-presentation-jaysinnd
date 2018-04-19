using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public GameObject gameOverpanel;
    public Text sText;
    int score = 0;
    public Text highScore;
    public Text currentScore;
    public GameObject newScoreAlert;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver()
    {
        Invoke("ShowOverPanel", 2.0f);
    }
    void ShowOverPanel()
    {
        sText.gameObject.SetActive(false);

        if (score > PlayerPrefs.GetInt("HighScore", 0)) //checks to see if the player has a new high score vs the old high score
        {
            PlayerPrefs.SetInt("HighScore", score);

            newScoreAlert.SetActive(true); //tells the new high score to become visible , along with the alert
        }
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString(); //sets the new high score
        currentScore.text = "Current Score: " + score.ToString();

        gameOverpanel.SetActive(true); //sets the Game over panel to visible upon collision with an object other than a coin
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevelName); //method called when a player wants to start over, it resets teh scene
    }
    public void ScoreCalc() //Function to increment player score +1 each time player touches a 'coin'
    {
        score++; //adds a count of 1 to the players score each time they touch a yellow coin
        sText.text = score.ToString();
    }
    public void QuitGame() //method to be used if player decides to quit the game
    {
        Application.Quit(); 
    }
}

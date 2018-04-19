using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public AudioClip gameEnd;
    public float scrollSpeed = 4.8f;
    public GameObject[] obstacles;
    public float frequency = 0.5f;
    float counter = 0.0f;
    public Transform obstaclesSpawn;
    bool gameOver = false;

    GameObject currentChild;

    // Use this for initialization
    void Start()
    {
        GenerateObstacle(); //calls the obstacles generator method to start sending obstacles our way
        Speedup(); //calls the speedup method
    }

    void Speedup() //method created to speed up the game after a certain time is achieved
    {
        StartCoroutine("IncreaseSpeed");
    }

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSecondsRealtime(60); //tells the game to wait 60s before speeding up the game
        scrollSpeed++; //after the number set (60 right now) is achieved, the game increments to a higher scrolling speed
        print(scrollSpeed);
        Speedup(); //calls the Speedup method
    }

    // Update is called once per frame
    void Update() //Generates our obstacles off screen to the right
    {
        if (gameOver)
        {
            scrollSpeed = 0;
            return;
        }

        if (counter <= 0.0f)
        {
            GenerateObstacle(); //calls the GenerateObstacle function
        }
        else
        {
            counter -= Time.deltaTime * frequency;
        }

        
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i).gameObject;
            Scroll(currentChild);

            if (currentChild.transform.position.x <= -15.0f) //Destroys obstacle objects once they are so far off screen
            {
                Destroy(currentChild);
            }
        }
    }

    void Scroll(GameObject currentChallenge)
    {
        currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }

    void GenerateObstacle() //Generates all our obstacles to challenge the player.
    {
        GameObject newObstacles = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstaclesSpawn.position, Quaternion.identity) as GameObject;
        newObstacles.transform.parent = transform;
        counter = 1.0f;
    }

    public void GameOver()
    {
        gameOver = true;
        transform.GetComponent<GameControl>().GameOver();
    }


}

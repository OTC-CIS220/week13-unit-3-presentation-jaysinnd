using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float jumpSize = 5.0f;
    Rigidbody2D iRigidBody;
    //bool isGrounded = false;
    bool isOnGround;
    bool gameOver = false;

    float positionX = 0.0f;
    
    Scroller myScroller;
    GameControl myGameControl;
    public AudioClip gameEnd;
    public AudioClip backGround;
    public AudioClip collide;
    public AudioClip jumpSound;
    public AudioClip scoreSound;
    AudioSource myAudioSource;

    //my stuff
    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.collider.name == "ground"){
            isOnGround = true;
        }
        else{
            isOnGround = false;
        }

        if (coll.collider.tag == "Enemy"){
            GameOver();
        }
    }

    //I changed the jump a bit.
    void jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400.0f));
        myAudioSource.PlayOneShot(jumpSound);
        isOnGround = false;
    }
        
    // Use this for initialization
    void Start () {
        iRigidBody = transform.GetComponent<Rigidbody2D>();
        positionX = transform.position.x;
        myScroller = GameObject.FindObjectOfType<Scroller>();
        myGameControl = GameObject.FindObjectOfType<GameControl>();
        myAudioSource = GameObject.FindObjectOfType<AudioSource>();
    }
	
	void FixedUpdate () {
        
        //statement to check if Player has collided with an Obstacle.
        //calls the GameOver function.
        if (iRigidBody.position.x < positionX) //calculates position of Player vs Obstacle
        {
            myAudioSource.PlayOneShot(collide);
            GameOver(); //Calls Game Over method
        }
		
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            jump();
        }

    }

    //The Game Over method - called when player gets 'hit' by obstacles
    void GameOver()
    {
        gameOver = true;
        positionX = positionX - .01f;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(gameEnd);
        myScroller.GameOver();
    }

    //Manage Collision Actions
    void OnCollisionStay2D (Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isOnGround = true; //Changes status of Player to true, allowing them to jump
        }
    }
    void OnCollisionExit2D (Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isOnGround = false; //Player is in mid-jump, CANNOT jump again.
        }
    }
    
    void OnTriggerEnter2D (Collider2D other) //For score keeping. See GameControl class
    {
        if (other.tag == "Coin")
        {
            myAudioSource.PlayOneShot(scoreSound);
            myGameControl.ScoreCalc(); //calls function ScoreCalc() from GameControl class.
            Destroy(other.gameObject); //destroys the 'coin' upon player collision.
            
        }
    }
}

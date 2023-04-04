using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    public float speed2;
    public GameObject timer;

    public int score; //holds the player's score
    public int maxPickups; //maximum amount of pickusp in scene
    public TMP_Text scoreText; //holds UI text object
    public TMP_Text timerText;
    bool reverse;
    bool wonGame = false;


    // Start is called before the first frame update
    void Start()
    {
        maxPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Get the rigidbody component of the gameObject 
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score: " + score + "/" + maxPickups;
        //timer = FindObjectOfType<Timer>();
        //timer.StartTimer;
        timer.GetComponent<Timer>().StartTimer();

    }

    // Update is called once per frame
    void Update()
    {


        if (wonGame == true)
            return;


        /*if (score < 100)
            if (reverse == false)
                score = score + 1;
        Debug.Log("Score is at" + score);
        if (score > 100)
            reverse = true;
        Debug.Log("Reverse is true");*/

        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from ou virtical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vetor 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our enviroment vector
        rb.AddForce(movement * speed * Time.deltaTime); ;

        //Debug.Log(Timer);



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pickup")
        {
            //whenever the player collides with the pickup
            //The pickup will be destroyed
            //the score will be increased
            //and the text in the UI will change to display the most recent score
            Destroy(other.gameObject);
            score++;
            scoreText.text = "Score: " + score + "/" + maxPickups;
            Debug.Log(score);
        }
    }
}

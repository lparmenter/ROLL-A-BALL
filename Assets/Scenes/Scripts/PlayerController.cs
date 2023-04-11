using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    int pickupCount;
    int totalPickups;
    int pickupCollected;
    Timer timer;
    bool wonGame = false;

    [Header("UI")]
    
    public TMP_Text wintime;
    public GameObject inGamePanel;
    public TMP_Text timerText;
    public TMP_Text pickupText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColor;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }


    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
            var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColor;
        resetting = false;
    }






    void Start()
    {
        //Get the rigidbody component of the gameObject
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Set the pickup count to the total
        pickupCount = totalPickups;
        pickupCollected = 0;
        //Display the pickup count
        //Turn off our  win panel
        gameOverScreen.SetActive(false);
        CheckPickups();
        //Get the Timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        //Turn on our in game panel
        inGamePanel.SetActive(true);

        //TEMPORARY/// Turns pause menu off at start
        pauseScreen.SetActive(false);

        resetPoint = GameObject.Find("ResetPoint");
        originalColor = GetComponent<Renderer>().material.color;


    }

    void Update()
    {
        if (wonGame == true)
            return;

        if (resetting)
            return;

        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from our vertical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our movement vector
        rb.AddForce(movement * speed * Time.deltaTime);
        //Update the timer text to that of the timer
        timerText.text = "Time:" + timer.GetTime().ToString("F3");

    }


    void OnTriggerEnter(Collider other)
    {
      
        //If the other object contains the Pickup tag, destroy it
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount -= 1;
            CheckPickups();
        }
    }

    void CheckPickups()
    {
        //Debug.Log("Pickups left: " + pickupCount);
        pickupText.text = "Pickups left: " + pickupCount;
        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        wonGame = true;
        timer.StopTimer();
        //Turn off our in game panel
        inGamePanel.SetActive(false);
        Debug.Log("You Win!!! Your time was: " + timer.GetTime().ToString("F3"));
        //Set the timer on the text
        wintime.text = "Your time was: " + timer.GetTime().ToString("F3");
        //Ture on our win panel
        gameOverScreen.SetActive(true);
        //Set the velocity of the ridgedbody to zero
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    //Temporary - Remove when doing mdules in A2
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


}

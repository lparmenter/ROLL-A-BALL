using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    public float speed2;

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody component of the gameObject 
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from ou virtical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vetor 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our enviroment vector
        rb.AddForce(movement * speed);

    }
}

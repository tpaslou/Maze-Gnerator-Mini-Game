using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimpleMove : MonoBehaviour
{
    public float speed=100;

    private Rigidbody rb;
    public bool GameEnd;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        GameEnd = false;
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameEnd")
        {
            Debug.Log("Game End !");
            GameEnd = true;
        }

    }
}

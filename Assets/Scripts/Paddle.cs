using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddles : MonoBehaviour
{
    public float speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        if (tag == "left")
        {
            transform.position = new Vector3(-11.3999996f,7.99f,19.2099991f);
        }
        else
        {
            transform.position = new Vector3(11.3999996f,7.99f,19.2099991f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool wKeyDown = Input.GetKey(KeyCode.W);
        bool sKeyDown = Input.GetKey(KeyCode.S);
        bool upKeyDown = Input.GetKey(KeyCode.UpArrow);
        bool downKeyDown = Input.GetKey(KeyCode.DownArrow);
        if (tag == "left")
        {
            if (wKeyDown)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(0, speed);
            }

            else if (sKeyDown)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(0, -speed);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            }
        }
        else
        {
            if (upKeyDown)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(0, speed);
            }

            else if (downKeyDown)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(0, -speed);
            }
            else 
            {                
                GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
            }
        }
    }
}

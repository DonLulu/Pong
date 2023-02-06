using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public GameObject LeftPaddle;
    public GameObject RightPaddle;
    public float speed = 15f;
    public float initialSpeed;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    public TMP_Text scoreP1;
    public TMP_Text scoreP2;
    public TMP_Text endMessage;
    public CameraShake camera;

        

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 7.98999977f, 19.5f);
        GetComponent<Rigidbody>().velocity = new Vector2(1, 0.2f) * speed;
        scoreP2.text = "0";
        scoreP1.text = "0";
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        scoreP1.text = playerOneScore.ToString();
        scoreP2.text = playerTwoScore.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "left")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y) * 1;
            speed+= 0.1f;
        }

        if (collision.transform.tag == "right")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y) * 1;
            speed+=0.1f;
        }

        if (collision.transform.tag == "upperWall")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y) * 1;
        }

        if (collision.transform.tag == "bottomWall")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y) * 1;
        }

    }

    IEnumerator Waiter5sec()
    {
        yield return new WaitForSeconds(5);
        endMessage.gameObject.SetActive(false);
        Restart();
    }
    IEnumerator Waiter1sec()
    {
        yield return new WaitForSeconds(1);
    }

    private void Restart()
    {
        transform.position = new Vector3(0f, 7.98999977f, 19.5f);
        GetComponent<Rigidbody>().velocity = new Vector2(1, 0.2f) * speed;
        speed = initialSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "rightWall")
        {
            playerOneScore++;
            if (playerOneScore == 11)
            {
                playerOneScore = 0;
                playerTwoScore = 0;
                endMessage.gameObject.SetActive(true);
                endMessage.text = "Game Over, Left Paddle Wins";
                StartCoroutine(Waiter5sec());
            }
            else
            {
                Restart();
            }
            print("Player 1 scored !\n Score : " + playerOneScore +" / " + playerTwoScore);
            
        }

        if (other.transform.tag == "leftWall")
        {
            playerTwoScore++;
            if (playerTwoScore == 11)
            {
                playerOneScore = 0;
                playerTwoScore = 0;
                endMessage.gameObject.SetActive(true);
                endMessage.text = "Game Over, Right Paddle Wins";
                StartCoroutine(Waiter5sec());
                endMessage.gameObject.SetActive(false);
            }
            else
            {
                Restart();
            }
            print("Player 2 scored !\n Score : " + playerOneScore +" / " + playerTwoScore);
        }
    }
}

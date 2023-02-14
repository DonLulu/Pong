using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class BallPong : MonoBehaviour
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
    public GameObject camera;
    private bool cameraShake;
    Vector3 maximumAngularShake = Vector3.one * 0.8f;
    public GameObject blueLight;
    public GameObject redLight;
    public AudioSource pongSound;
    public AudioSource winSound;
    private float frequency = 25;
    private float pitch = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 7.99f, 19.5f);
        GetComponent<Rigidbody>().velocity = new Vector2(1, 0.2f) * speed;
        scoreP2.text = "0";
        scoreP1.text = "0";
        initialSpeed = speed;
        cameraShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerOneScore > 3)
            scoreP1.color = Color.yellow;
        if(playerOneScore > 7)
            scoreP1.color = Color.red;
        if(playerTwoScore > 3)
            scoreP2.color = Color.yellow;
        if(playerTwoScore > 7)
            scoreP2.color = Color.red;
        scoreP1.text = playerOneScore.ToString();
        scoreP2.text = playerTwoScore.ToString();
        
        //Camera shake
        if (cameraShake)
        {
            camera.transform.localRotation = Quaternion.Euler(new Vector3(
                maximumAngularShake.x * (Mathf.PerlinNoise(3, Time.time * frequency) * 2 - 1),
                maximumAngularShake.y * (Mathf.PerlinNoise(4, Time.time * frequency) * 2 - 1),
                maximumAngularShake.z * (Mathf.PerlinNoise(5, Time.time * frequency) * 2 - 1)));
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "left")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y) * 1;
            speed+= 0.1f;
            cameraShake = true;
            StartCoroutine(WaiterShake());
            pitch += 0.1f;
        }

        if (collision.transform.tag == "right")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y) * 1;
            speed+=0.1f;
            cameraShake = true;
            StartCoroutine(WaiterShake());
            pitch += 0.1f;
        }

        if (collision.transform.tag == "upperWall")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y) * 1;
            cameraShake = true;
            StartCoroutine(WaiterShake());
        }

        if (collision.transform.tag == "bottomWall")
        {
            GetComponent<Rigidbody>().velocity =
                new Vector2(GetComponent<Rigidbody>().velocity.x, -GetComponent<Rigidbody>().velocity.y) * 1;
            cameraShake = true;
            StartCoroutine(WaiterShake());
        }
        pongSound.pitch = pitch;
        pongSound.Play();
    }

    IEnumerator Waiter5sec()
    {
        yield return new WaitForSeconds(5);
        endMessage.gameObject.SetActive(false);
        Restart();
    }
    IEnumerator WaiterShake()
    {
        yield return new WaitForSeconds(0.1f);
        cameraShake = false;
        camera.transform.position = new Vector3(0, 8.28999996f, 3.11999989f);
    }
    IEnumerator WaiterLightBlue()
    {
        blueLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        blueLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blueLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        blueLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blueLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        blueLight.SetActive(true);
        
    }
    IEnumerator WaiterLightRed()
    {
        redLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        redLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        redLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        redLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        redLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        redLight.SetActive(true);
        
    }
    private void Restart()
    {
        transform.position = new Vector3(0,7.99f,19.5f);
        GetComponent<Rigidbody>().velocity = new Vector2(1, 0.2f) * speed;
        speed = initialSpeed;
        pitch = 1f;
        scoreP1.color = Color.white;
        scoreP2.color = Color.white;
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
                StartCoroutine(WaiterLightBlue());
            }
            else
            {
                Restart();
                StartCoroutine(WaiterLightBlue());
            }
            print("Player 1 scored !\n Score : " + playerOneScore +" / " + playerTwoScore);
            winSound.Play();

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
                StartCoroutine(WaiterLightRed());
            }
            else
            {
                Restart();
                StartCoroutine(WaiterLightRed());
            }
            print("Player 2 scored !\n Score : " + playerOneScore +" / " + playerTwoScore);
            winSound.Play();
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    private float tileLength;
    private float startPosition;
    public float travelSpeed = -0.1f;  //where should we be storing the player's current speed if it's going to increase as the game goes on?
    public float parallaxAmount;
    private float travelDistance;
    public GameManager gameManager;
    public GameObject speedController;


    void Start()
    {
        startPosition = transform.position.x;
        tileLength = GetComponent<SpriteRenderer>().bounds.size.x;
        gameManager = FindObjectOfType<GameManager>();
        //Debug.Log(tileLength);
    }

    void FixedUpdate()
    {

        //must get the player's travel speed
        if (gameManager.GameStarted == true)
        {
            travelSpeed = speedController.GetComponent<SpeedController>().moveSpeed;
            travelDistance = travelDistance + (travelSpeed * parallaxAmount);
            if (travelDistance < -tileLength) //this is in negatives because the background is scrolling, not the player
            {
                //Debug.Log("tile length exceeded");
                travelDistance = 0;
            }
            transform.position = new Vector3(startPosition + travelDistance, transform.position.y, transform.position.z);
            //reset when we have moved one tile's length

        }

    }
}

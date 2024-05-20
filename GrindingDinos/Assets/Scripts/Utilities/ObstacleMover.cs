using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private float travelSpeed = -0.1f; //link this to the player's speed when we're done testing
    public GameManager gameManager;
    public GameObject speedController;
    // Start is called before the first frame update
    void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
    }


    void FixedUpdate()
    {
        travelSpeed = speedController.GetComponent<SpeedController>().moveSpeed;
        if (gameManager.GameStarted == true)
        transform.position = new Vector3(transform.position.x + travelSpeed, transform.position.y, transform.position.z);
    }

    public void ObstacleCreated(GameManager gm, GameObject speedCont)
    {
        gameManager = gm;
        speedController = speedCont;
    }
}

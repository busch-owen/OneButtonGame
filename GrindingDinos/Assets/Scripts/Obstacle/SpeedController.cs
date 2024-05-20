using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float moveSpeed = -0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpeedupTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpeedupTimer()
    {
        yield return new WaitForSeconds(10);
        moveSpeed = moveSpeed - 0.05f;
        StartCoroutine(SpeedupTimer());
    }
}

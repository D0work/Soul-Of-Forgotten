using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("MovePoints")]
    public Transform pointA;
    public Transform pointB;

    [Header("Settings")]
    public float speedMultiplier = 0.5f;
    public float waitTime = 1f;

    public bool isWaiting = false;
    private float timeWaiting = 0f;
    private bool movingToward = true;
    private float percentMoved = 0f;

    public bool OneTImeOnly = false;
    public bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isWaiting || IsPaused)
        {
            Wait();
        }
        else
        {
            Move();
        }
    }

    private void Wait()
    {
        timeWaiting += Time.deltaTime;
        if (timeWaiting >= waitTime)
        {
            timeWaiting = 0f;
            isWaiting = false;
        }
    }

    private void Move() {
        if (movingToward)
        {
            percentMoved += Time.deltaTime * speedMultiplier;
            if (percentMoved >= 1)
            {
                isWaiting = true;
                movingToward = false;
            }
        }
        else if (!OneTImeOnly)
        {
            percentMoved -= Time.deltaTime * speedMultiplier;
            if (percentMoved <=0)
            {
                isWaiting = true;
                movingToward = true;
            }
        }
        transform.position = Vector3.Lerp(pointA.position, pointB.position, percentMoved);
    }

    public void restart()
    {
        IsPaused = false;
    }
}

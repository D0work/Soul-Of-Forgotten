using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Activate : MonoBehaviour
{
    [Header("Scene Start Events")]
    [Tooltip("Invoke once in Awake")]
    public UnityEvent onAwake;
    [Tooltip("Invoke only once in Awake?")]
    public bool awakeOnce = false;
    private bool hasAwoken = false;

    [Tooltip("Invoke once in Start")]
    public UnityEvent onStart;
    [Tooltip("Invoke only once in Start?")]
    public bool startOnce = false;
    private bool hasStarted = false;

    [Header("Trigger Events")]
    [Tooltip("Invoke when an object enters this trigger")]
    public UnityEvent onTriggerEnter;
    [Tooltip("Invoke only once on enter?")]
    public bool enterOnce = false;
    private bool hasEntered = false;

    [Tooltip("Invoke when an object exits this trigger")]
    public UnityEvent onTriggerExit;
    [Tooltip("Invoke only once on exit?")]
    public bool exitOnce = false;
    private bool hasExited = false;

    private void Awake()
    {
        if (awakeOnce)
        {
            if (!hasAwoken)
            {
                onAwake.Invoke();
                hasAwoken = true;
            }
        }
        else
        {
            onAwake.Invoke();
        }
    }

    private void Start()
    {
        if (startOnce)
        {
            if (!hasStarted)
            {
                onStart.Invoke();
                hasStarted = true;
            }
        }
        else
        {
            onStart.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enterOnce)
        {
            if (!hasEntered)
            {
                onTriggerEnter.Invoke();
                hasEntered = true;
            }
        }
        else
        {
            onTriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exitOnce)
        {
            if (!hasExited)
            {
                onTriggerExit.Invoke();
                hasExited = true;
            }
        }
        else
        {
            onTriggerExit.Invoke();
        }
    }
}

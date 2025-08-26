using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDeActive : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The lifetime of this gameobject")]
    public float lifetime = 5.0f;

    // The amount of time this gameobject has already existed in play mode
    public float timeAlive = 0.0f;

    [Tooltip("Whether to destroy child gameobjects when this gameobject is destroyed")]
    public bool destroyChildrenOnDeath = true;

    // Flag which tells whether the application is shutting down (helps avoid errors)
    public static bool quitting = false;

    public bool isDegraded = false;
    [SerializeField] private float deformStrength = 0.1f;
    public float degradeInterval = 1.0f;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] degradedVertices;

    /// <summary>
    /// Description:
    /// Standard Unity function called when the game is being quit or the play mode is exited
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    private void OnApplicationQuit()
    {
        // Ensures that the quitting flag gets set correctly to avoid work as the application quits
        quitting = true;
        DetachChildren();
        DestroyImmediate(this.gameObject);
    }

    void Start()
    {
        if (isDegraded)
        {
            mesh = GetComponent<MeshFilter>().mesh;
            originalVertices = mesh.vertices;
            degradedVertices = new Vector3[originalVertices.Length];
            originalVertices.CopyTo(degradedVertices, 0);
            InvokeRepeating(nameof(DegradeMesh), degradeInterval, degradeInterval);
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once per frame
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    void Update()
    {
        // Every frame, increment the amount of time that this gameobject has been alive,
        // or if it has exceeded it's maximum lifetime, destroy it
        if (timeAlive > lifetime)
        {
            //DetachChildren();
            gameObject.SetActive(false); 
        }
        else
        {
            timeAlive += Time.deltaTime;

        }
    }

    /// <summary>
    /// Description:
    /// Detaches children if the setting was set to true
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    private void DetachChildren()
    {
        if (destroyChildrenOnDeath && !quitting && Application.isPlaying)
        {
            int childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject childObject = transform.GetChild(i).gameObject;
                if (childObject != null)
                {
                    childObject.SetActive(false);
                }
            }
        }

        transform.DetachChildren(); 
    }

    void DegradeMesh()
    {
        for (int i = 0; i < degradedVertices.Length; i++)
        {
            degradedVertices[i] += Random.insideUnitSphere * deformStrength;
        }

        mesh.vertices = degradedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}

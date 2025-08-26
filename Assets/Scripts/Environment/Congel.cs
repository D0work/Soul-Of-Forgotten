using System.Collections.Generic;
using UnityEngine;
using static ThirdPersonCharacterController;

public class Congel : MonoBehaviour
{
    public List<GameObject> objectsToFreeze;
    public List<GameObject> objectsToDesactivate;
    public Material iceMaterial;

    public bool playerInside = false;
    public ThirdPersonCharacterController player;
    public Attributes a_player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            player = other.GetComponent<ThirdPersonCharacterController>();
            a_player = other.GetComponent<Attributes>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            player = null;
            a_player = null;
        }
    }

    void Update()
    {
        if (!playerInside) return;

        bool hasIce = a_player.ice;
        bool isCasting = player.playerState == PlayerState.LCasting;
        bool cPressed = player.InvokeInput.IsPressed();

        if (hasIce && (isCasting || cPressed))
        {
            FreezeObjects();
        }
    }

    void FreezeObjects()
    {
        foreach (GameObject obj in objectsToFreeze)
        {
            Renderer rend = obj.GetComponent<Renderer>();
            if (rend != null && iceMaterial != null)
            {
                rend.material = iceMaterial;
            }
        }

        foreach (GameObject obj in objectsToDesactivate)
        {
            obj.SetActive(false);
        }
    }
}

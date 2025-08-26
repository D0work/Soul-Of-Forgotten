using UnityEngine;
using static ThirdPersonCharacterController;

public class Burn : MonoBehaviour
{
    [Tooltip("burn")]
    public GameObject burnEffect;

    [Tooltip("script")]
    public MonoBehaviour scriptToActivate;

    public bool playerInside = false;
    public Attributes a_player;
    public ThirdPersonCharacterController player;

    void OnTriggerEnter(Collider other)
    {
        playerInside = true;
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    void Update()
    {
        if (!playerInside) return;

        bool hasFire = a_player.fire;
        bool isCasting = player.playerState == PlayerState.RCasting;
        bool cPressed = player.CastInput.IsPressed();

        if (hasFire && (isCasting || cPressed))
        {
            ActivateBurn();
        }
    }

    void ActivateBurn()
    {
        if (burnEffect != null)
            burnEffect.SetActive(true);

        if (scriptToActivate != null)
            scriptToActivate.enabled = true;
    }
}

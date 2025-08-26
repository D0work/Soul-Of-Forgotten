using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class RevealController : MonoBehaviour
{
    [Header("Speed")]
    public float slowSpeed = 2f;
    public float normalSpeed = 10f;
    public float slowJump = 5f;
    public float normalJump = 15f;
    public bool changeStatus = false;

    [Header("Light")]
    public Light playerLight;
    public float minIntensity = 0.2f;
    public float minRange = 2f;
    public float revealIntensity = 1.5f;
    public float revealRange = 10f;
    public GameObject fireEffect = null;
    public GameObject windEffect = null;
    public GameObject iceEffect = null;

    private ThirdPersonCharacterController ctrl;
    private Attributes attributes;
    private bool revealed = false;

    void Start()
    {
        ctrl = GetComponentInParent<ThirdPersonCharacterController>();
        attributes = GetComponentInParent<Attributes>();

        if (changeStatus) { 
            ctrl.moveSpeed = slowSpeed;
            ctrl.jumpStrength = slowJump;
        }

        if (playerLight != null)
        {
            playerLight.intensity = minIntensity;
            playerLight.range = minRange;
        }
    }

    void Update()
    {
        bool _input = ctrl.BuffInput.IsPressed();
        bool _hasFire = attributes.fire;
        bool _hasWind = attributes.wind;
        bool _hasIce = attributes.ice;

        if (!revealed && _input)
        {
            RevealArea(_hasFire,_hasWind,_hasIce);
        }
    }

    void RevealArea(bool canLight, bool canRestore, bool canGel)
    {
        revealed = true;

        if (canRestore)
        {
            ctrl.moveSpeed = normalSpeed;
            ctrl.jumpStrength = normalJump;
            windEffect?.SetActive(true);
        }

        if (iceEffect != null && canGel)
        {
            ctrl.moveSpeed = normalSpeed;
            ctrl.jumpStrength = normalJump;
            iceEffect?.SetActive(true);

            playerLight.intensity = revealIntensity;
            playerLight.range = revealRange;
            fireEffect?.SetActive(true);
        }

        if (playerLight != null && canLight)
        {
            playerLight.intensity = revealIntensity;
            playerLight.range = revealRange;
            fireEffect?.SetActive(true);
        }
    }
}

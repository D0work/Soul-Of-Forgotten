using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    [Tooltip("Player avatar representation to transform")]
    public GameObject PlayerRepresentation;
    private PlayerRepresentation playerRepresentationScript;
    private ThirdPersonCharacterController characterController;

    [Tooltip("Transform effect prefab")]
    public GameObject TransformationEffect;
    public GameObject TransformationSoundEffect;

    [Tooltip("Transform ordered avatar loading")]
    public GameObject[] Avatars;

    [Tooltip("Level of transformation for the current transform")]
    public int TransformIndex = 0;

    [Tooltip("Duration of the transformation effect in seconds")]
    public float effectDuration = 2f;


    void Start()
    {
        playerRepresentationScript = PlayerRepresentation.GetComponent<PlayerRepresentation>();
        TransformIndex = AttributeManager.attributeInstance.level;
        characterController = GetComponent<ThirdPersonCharacterController>();
        SetPlayer();
    }

    void SetPlayer()
    {
        foreach (GameObject avatar in Avatars)
        {
            avatar.SetActive(false);
        }

        Avatars[TransformIndex].SetActive(true);
        PlayerRepresentation = Avatars[TransformIndex];
        playerRepresentationScript.representationAnimator = Avatars[TransformIndex].GetComponent<Animator>();
    }


    public void Playertransform()
    {
        TransformIndex++;
        AttributeManager.attributeInstance.level = (AttributeManager.attributeInstance.level + 1) % Avatars.Length;

        if (TransformIndex >= Avatars.Length)
            return;

        StartCoroutine(DoTransformation());
    }

    private IEnumerator DoTransformation()
    {
        if (characterController != null)
            characterController.enabled = false;

        if (PlayerRepresentation != null)
            PlayerRepresentation.SetActive(false);

        if (TransformationEffect != null)
        {
            GameObject effect = Instantiate(TransformationEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectDuration);
        }

        if (TransformationSoundEffect != null)
        {
            GameObject effect = Instantiate(TransformationSoundEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectDuration);
        }

        yield return new WaitForSeconds(effectDuration);

        if (Avatars[TransformIndex] != null)
        {
            Avatars[TransformIndex].SetActive(true);
            PlayerRepresentation = Avatars[TransformIndex];
            playerRepresentationScript.representationAnimator = Avatars[TransformIndex].GetComponent<Animator>();
        }

        if (characterController != null)
            characterController.enabled = true;
    }
}

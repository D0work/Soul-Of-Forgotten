using UnityEngine;
using Cinemachine;
using System.Collections;

public class FocusZones : MonoBehaviour
{
    [Header("Références")]
    public CinemachineFreeLook freeLookCam;
    public Transform zoneTarget;

    [Header("Réglages")]
    public float focusDuration = 2f;

    private Transform originalFollow;
    private Transform originalLookAt;
    private float originalXAxisMaxSpeed;
    private float originalYAxisMaxSpeed;

    void Awake()
    {
        // Save
        originalFollow = freeLookCam.Follow;
        originalLookAt = freeLookCam.LookAt;
        originalXAxisMaxSpeed = freeLookCam.m_XAxis.m_MaxSpeed;
        originalYAxisMaxSpeed = freeLookCam.m_YAxis.m_MaxSpeed;
    }
    public void SetFocus(Transform focusTarget)
    {
        zoneTarget = focusTarget;      
        TriggerFocusZone();            
    }

    public void TriggerFocusZone()
    {
        StopAllCoroutines();
        StartCoroutine(FocusSequence());
    }

    private IEnumerator FocusSequence()
    {
        // Deactivation
        freeLookCam.m_XAxis.m_MaxSpeed = 0f;
        freeLookCam.m_YAxis.m_MaxSpeed = 0f;

        // Switch
        freeLookCam.Follow = zoneTarget;
        freeLookCam.LookAt = zoneTarget;

        // wait
        yield return new WaitForSeconds(focusDuration);

        // return
        freeLookCam.Follow = originalFollow;
        freeLookCam.LookAt = originalLookAt;
        freeLookCam.m_XAxis.m_MaxSpeed = originalXAxisMaxSpeed;
        freeLookCam.m_YAxis.m_MaxSpeed = originalYAxisMaxSpeed;
    }

    void OnDisable()
    {
        freeLookCam.Follow = originalFollow;
        freeLookCam.LookAt = originalLookAt;
        freeLookCam.m_XAxis.m_MaxSpeed = originalXAxisMaxSpeed;
        freeLookCam.m_YAxis.m_MaxSpeed = originalYAxisMaxSpeed;
    }
}

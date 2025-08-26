using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    [Header("Settings")]
    public Transform target;
    public float travelDuration = 1f;
    public float waitDuration = 2f;

    private Transform initialParent;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Coroutine currentRoutine;

    public void StartMove()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        // 1) Save
        initialParent = transform.parent;
        transform.SetParent(null, false);
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // 2) Move
        yield return SmoothLerp(
            transform.position,
            transform.rotation,
            target.position,
            target.rotation,
            travelDuration
        );

        // 3) Pause
        yield return new WaitForSeconds(waitDuration);

        // 4) Return
        yield return SmoothLerp(
            transform.position,
            transform.rotation,
            initialPosition,
            initialRotation,
            travelDuration
        );

        // 5) Attach
        transform.SetParent(initialParent, false);
        transform.localPosition = initialParent != null
            ? initialParent.InverseTransformPoint(initialPosition)
            : initialPosition;
        transform.localRotation = initialParent != null
            ? Quaternion.Inverse(initialParent.rotation) * initialRotation
            : initialRotation;
    }

    private IEnumerator SmoothLerp(
        Vector3 fromPos, Quaternion fromRot,
        Vector3 toPos, Quaternion toRot,
        float duration)
    {
        Debug.DrawLine(fromPos, toPos, Color.green, duration);

        Debug.Log($"SmoothLerp ▶ from {fromPos} to {toPos}");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            transform.position = Vector3.Lerp(fromPos, toPos, t);
            transform.rotation = Quaternion.Slerp(fromRot, toRot, t);

            yield return null;
        }
        transform.position = toPos;
        transform.rotation = toRot;
    }
}

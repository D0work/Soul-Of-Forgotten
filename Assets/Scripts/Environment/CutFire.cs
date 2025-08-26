using UnityEngine;
using System.Collections;

public class CutFire : MonoBehaviour
{
    public float delay = 5f;

    private void OnEnable()
    {
        StartCoroutine(DestroyAllFiresAfterDelay());
    }

    private IEnumerator DestroyAllFiresAfterDelay()
    {
        Fire[] fires = FindObjectsOfType<Fire>();
        yield return new WaitForSeconds(delay);
        foreach (var fire in fires)
        {
            if (fire != null)
                Destroy(fire.gameObject);
        }
    }
}

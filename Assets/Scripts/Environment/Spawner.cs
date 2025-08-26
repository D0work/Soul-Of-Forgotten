using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject prefab, Transform spawnPoint, Transform target, float speed, float spread)
    {
        Vector3 dir = (target.position - spawnPoint.position).normalized;
        dir += Random.insideUnitSphere * spread;
        dir.Normalize();
        Quaternion rot = Quaternion.LookRotation(dir);
        GameObject proj = Instantiate(prefab, spawnPoint.position, rot);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = proj.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
        rb.velocity = dir * speed;
    }
}

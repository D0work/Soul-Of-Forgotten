using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DragonController : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;
    public Transform target;
    public GameObject firePrefab;
    public GameObject rockPrefab;
    public Transform[] rockSpawnPoints;
    public Spawner spawner;

    [Header("Timing & Counts")]
    public float fireInterval = 1f;
    public float rockInterval = 0.5f;
    public int rockCount = 6;
    public float fireStateDuration = 5f;
    public float runCooldown = 2f;
    public float returnDelay = 0.5f;

    [Header("Speeds & Spread")]
    public float fireSpeed = 10f;
    public float fireSpread = 0.1f;
    public float rockSpeed = 8f;
    public float rockSpread = 0.2f;
    public float runSpeed = 5f; 

    Animator animator;
    Vector3 startPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            if (Random.value < 0.5f)
                yield return FireState();
            else
                yield return RunState();

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FireState()
    {
        animator.CrossFade("FlyingAttack", 0.1f);
        Coroutine rocks = StartCoroutine(SpawnRocks());
        Coroutine fire = StartCoroutine(SpawnFire());

        yield return new WaitForSeconds(fireStateDuration);

        StopCoroutine(rocks);
        StopCoroutine(fire);
    }

    IEnumerator SpawnRocks()
    {
        int index = 0;
        while (true)
        {
            var spawnTransform = rockSpawnPoints[index];
            spawner.Spawn(rockPrefab, spawnTransform, target, rockSpeed, rockSpread);

            index = (index + 1) % rockSpawnPoints.Length;
            yield return new WaitForSeconds(rockInterval);
        }
    }

    IEnumerator SpawnFire()
    {
        while (true)
        {
            spawner.Spawn(firePrefab, spawnPoint, target, fireSpeed, fireSpread);
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator RunState()
    {
        animator.CrossFade("FlyingFWD", 0.1f);

        yield return MoveSmoothly(transform.position, target.position, runSpeed);
        yield return new WaitForSeconds(returnDelay);
        yield return MoveSmoothly(transform.position, startPosition, runSpeed);
        yield return new WaitForSeconds(runCooldown);

        animator.CrossFade("IdleAgressive", 0.1f);
    }

    IEnumerator MoveSmoothly(Vector3 from, Vector3 to, float speed)
    {
        float distance = Vector3.Distance(from, to);
        float elapsed = 0f;
        float duration = distance / speed;
        Transform selfT = transform;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            selfT.position = Vector3.Lerp(from, to, t);

            if (Vector3.Distance(selfT.position, to) < 0.1f)
            {
                selfT.position = to;
                yield break;
            }

            yield return null;
        }

        selfT.position = to;
    }
}

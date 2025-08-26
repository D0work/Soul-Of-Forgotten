using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [Header("respawn")]
    public Transform spawnPoint;
    public GameObject player;
    private Health playerHealth;

    void Awake()
    {
        spawnPoint = this.transform;
        playerHealth = player.GetComponentInChildren<Health>();
    }

    void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnDie += HandleRespawn;
    }

    void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnDie -= HandleRespawn;
    }

    private void HandleRespawn()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }
}

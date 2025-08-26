using UnityEngine;

public class EnemyResetTrigger : MonoBehaviour
{
    public GameObject enemyRoot;
    public Transform enemyStartPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyRoot != null && enemyStartPosition != null)
        {
            enemyRoot.transform.position = enemyStartPosition.position;
            enemyRoot.transform.rotation = enemyStartPosition.rotation;

            var anim = enemyRoot.GetComponent<Animator>();
            if (anim != null)
                anim.CrossFade("IdleAgressive", 0.1f);
        }
    }
}

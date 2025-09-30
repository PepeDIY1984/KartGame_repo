using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitted : MonoBehaviour
{
    public GameObject enemyCollider;
    public GameObject enemyGeometry;
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerBullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject,.5f);
            enemyGeometry.SetActive(false);
            enemyCollider.GetComponent<BoxCollider>().enabled = false;
            explosion.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerVisibility : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<EnemyShot>().startShooting = true;
            gameObject.GetComponentInParent<Billboard>().enabled = true;
            gameObject.GetComponentInParent<EnemyShot>().shotToPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<EnemyShot>().startShooting = false;
            gameObject.GetComponentInParent<Billboard>().enabled = false;
        }
    }

}

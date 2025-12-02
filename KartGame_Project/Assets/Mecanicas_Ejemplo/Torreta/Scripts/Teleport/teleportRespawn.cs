using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportRespawn : MonoBehaviour
{

    public Transform teleport;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = teleport.position;
            other.GetComponent<CharacterController>().enabled = true;

        }

    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAImovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                navMeshAgent.destination = hit.point;
            }
        }
    }
}

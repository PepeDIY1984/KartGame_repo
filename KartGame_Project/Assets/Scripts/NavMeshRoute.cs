using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshRoute : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] checkpoints;
    public float switchDistance = 2.5f;     // distancia para cambiar al siguiente
    public float stoppingDistanceBuffer = 0.5f;

    private NavMeshAgent agent;
    private int currentIndex = 0;
    private bool routeCompleted = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Configuración recomendada para movimiento suave
        agent.autoBraking = false;
        agent.updateRotation = true;
        agent.acceleration = 25f;
        agent.angularSpeed = 300f;
        agent.stoppingDistance = stoppingDistanceBuffer;

        if (checkpoints != null && checkpoints.Length > 0)
        {
            SetNextDestination();
        }
    }

    void Update()
    {
        if (routeCompleted || checkpoints == null || checkpoints.Length == 0)
            return;

        // Solo comprobar cuando el path esté calculado
        if (!agent.pathPending)
        {
            // remainingDistance es más preciso que Vector3.Distance
            if (agent.remainingDistance <= switchDistance)
            {
                currentIndex++;
                if (currentIndex < checkpoints.Length)
                {
                    SetNextDestination();
                }
                else
                {
                    agent.isStopped = true;
                    routeCompleted = true;
                    Debug.Log("Ruta completada 🏁");
                }
            }
        }
    }

    private void SetNextDestination()
    {
        if (currentIndex < checkpoints.Length && checkpoints[currentIndex] != null)
        {
            agent.SetDestination(checkpoints[currentIndex].position);
        }
    }

    void OnDrawGizmos()
    {
        if (checkpoints == null || checkpoints.Length == 0) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < checkpoints.Length - 1; i++)
        {
            if (checkpoints[i] && checkpoints[i + 1])
                Gizmos.DrawLine(checkpoints[i].position, checkpoints[i + 1].position);
        }
    }
}

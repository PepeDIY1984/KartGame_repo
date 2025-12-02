using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controla un NavMeshAgent de forma suave usando Rigidbody para movimiento físico.
/// - Evita los “golpes” o saltos visuales.
/// - Compatible con colisiones.
/// - Ideal para NPCs, coches o bots que necesitan interactuar con el entorno.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class SmoothNavMeshAgentHybrid : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] checkpoints;
    public float switchDistance = 2f;
    public float moveSmoothness = 10f;    // Cuanto mayor, más pegado al path
    public float turnSmoothness = 10f;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private int currentIndex = 0;
    private bool routeCompleted = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        // --- Configuración del NavMeshAgent ---
        agent.updatePosition = false; // Desactiva el control directo del transform
        agent.updateRotation = false;
        agent.autoBraking = false;
        agent.acceleration = 25f;
        agent.angularSpeed = 400f;

        // --- Configuración del Rigidbody ---
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (checkpoints != null && checkpoints.Length > 0)
            agent.SetDestination(checkpoints[0].position);
    }

    void Update()
    {
        if (routeCompleted || checkpoints == null || checkpoints.Length == 0)
            return;

        // Solo procesar si el agente no está calculando su camino
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= switchDistance)
            {
                currentIndex++;

                if (currentIndex < checkpoints.Length)
                    agent.SetDestination(checkpoints[currentIndex].position);
                else
                {
                    routeCompleted = true;
                    agent.isStopped = true;
                    Debug.Log("Ruta completada 🏁");
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Movimiento suave hacia la posición del NavMeshAgent
        Vector3 nextPos = agent.nextPosition;
        Vector3 smoothedPos = Vector3.Lerp(rb.position, nextPos, moveSmoothness * Time.fixedDeltaTime);
        rb.MovePosition(smoothedPos);

        // Rotación suave hacia la dirección de movimiento deseada
        if (agent.desiredVelocity.sqrMagnitude > 0.01f)
        {
            Quaternion desiredRot = Quaternion.LookRotation(agent.desiredVelocity.normalized, Vector3.up);
            Quaternion smoothedRot = Quaternion.Slerp(rb.rotation, desiredRot, turnSmoothness * Time.fixedDeltaTime);
            rb.MoveRotation(smoothedRot);
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

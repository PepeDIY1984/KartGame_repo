using UnityEngine;
using UnityEngine.AI;

public class NavMeshRoute : MonoBehaviour
{
    public Transform[] checkpoints;        // Lista de checkpoints en orden
    public float switchDistance = 3f;      // Distancia para cambiar al siguiente checkpoint
    public float stoppingDistanceBuffer = 0.5f; // Extra para evitar frenadas en seco

    private NavMeshAgent agent;
    private int currentIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ajustes recomendados para suavizar
        agent.autoBraking = false;   // No frenar en cada destino, mantiene velocidad
        agent.updateRotation = true; // Deja que el agente gire suavemente
        agent.acceleration = 20f;    // Aceleración más alta para arrancar suave
        agent.angularSpeed = 400f;   // Giro más fluido
        agent.stoppingDistance = stoppingDistanceBuffer;

        if (checkpoints.Length > 0)
        {
            agent.SetDestination(checkpoints[currentIndex].position);
        }
    }

    void Update()
    {
        if (checkpoints.Length == 0 || currentIndex >= checkpoints.Length) return;

        // Distancia al checkpoint actual
        float distance = Vector3.Distance(transform.position, checkpoints[currentIndex].position);

        // Cambiar al siguiente antes de llegar al centro
        if (distance < switchDistance)
        {
            currentIndex++;

            if (currentIndex < checkpoints.Length)
            {
                agent.SetDestination(checkpoints[currentIndex].position);
            }
            else
            {
                Debug.Log("Ruta completada 🏁");
                agent.isStopped = true;
            }
        }
    }

    // Dibujar líneas en el editor
    void OnDrawGizmos()
    {
        if (checkpoints == null || checkpoints.Length == 0) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < checkpoints.Length - 1; i++)
        {
            if (checkpoints[i] != null && checkpoints[i + 1] != null)
            {
                Gizmos.DrawLine(checkpoints[i].position, checkpoints[i + 1].position);
            }
        }
    }
}

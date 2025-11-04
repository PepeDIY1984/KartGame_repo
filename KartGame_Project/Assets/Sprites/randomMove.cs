using UnityEngine;

public class randomMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 3f;           // Velocidad de movimiento
    public float changeDirectionTime = 2f; // Cada cuántos segundos cambia de dirección
    public float areaRadius = 10f;         // Radio máximo alrededor del punto inicial

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;

    private Vector3 originalScale;

    void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;
        ChooseNewTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Mover hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotar visualmente según dirección (flip X)
        Vector3 direction = targetPosition - transform.position;
        if (Mathf.Abs(direction.x) > 0.01f)
        {
            // Si se mueve hacia la izquierda → invierte X
            if (direction.x < 0)
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            else
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // Si llegó o toca cambiar de dirección
        if (timer >= changeDirectionTime || Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            ChooseNewTarget();
        }
    }

    void ChooseNewTarget()
    {
        timer = 0f;

        // Elegir nueva dirección aleatoria en el plano XZ
        Vector2 randomDir = Random.insideUnitCircle * areaRadius;
        targetPosition = new Vector3(startPosition.x + randomDir.x, startPosition.y, startPosition.z + randomDir.y);
    }

    // Dibuja el área en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? startPosition : transform.position, areaRadius);
    }
}

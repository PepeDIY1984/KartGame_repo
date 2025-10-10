using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Velocidad de movimiento hacia adelante/atrás
    public float turnSpeed = 100f;     // Velocidad de giro

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Para evitar que se caiga si no quieres físicas verticales
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Input
        float moveInput = Input.GetAxis("Vertical");   // W/S o flechas ↑↓
        float turnInput = Input.GetAxis("Horizontal"); // A/D o flechas ←→

        // Movimiento hacia adelante/atrás en la dirección local Z
        Vector3 moveDirection = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotación sobre el eje Y
        Quaternion turnRotation = Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
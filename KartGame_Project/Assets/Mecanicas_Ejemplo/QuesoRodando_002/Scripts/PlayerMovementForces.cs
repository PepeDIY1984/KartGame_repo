using UnityEngine;

public class PlayerMovementForces : MonoBehaviour
{
    public float moveForce = 15f;    // Fuerza hacia adelante/atrás
    public float turnTorque = 0.5f;    // Fuerza de giro (torque)
    public float maxSpeed = 10f;     // Velocidad máxima

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Input
        float moveInput = Input.GetAxis("Vertical");   // W/S o ↑↓
        float turnInput = Input.GetAxis("Horizontal"); // A/D o ←→

        // Aplicar fuerza en la dirección local Z (adelante/atrás)
        if (rb.linearVelocity.magnitude < maxSpeed || moveInput < 0) // Limitar velocidad máxima solo hacia adelante
        {
            rb.AddForce(transform.forward * moveInput * moveForce);
        }

        // Aplicar torque para rotar en el eje Y (izquierda/derecha)
        rb.AddTorque(Vector3.up * turnInput * turnTorque);
    }
}


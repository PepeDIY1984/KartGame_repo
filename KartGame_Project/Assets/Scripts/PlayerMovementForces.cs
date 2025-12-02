using UnityEngine;

public class PlayerMovementForces : MonoBehaviour
{
    public float moveForce = 15f;       // Fuerza hacia adelante/atrás
    public float turnSpeed = 100f;      // Velocidad de giro controlada
    public float maxSpeed = 10f;        // Velocidad máxima

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Evitar que rote en X o Z por choques
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Ajustar la resistencia al giro y al movimiento
        rb.angularDamping = 5f;   // Sustituye a angularDrag
        rb.linearDamping = 0.5f;  // Sustituye a drag

        // Limitar velocidad angular para evitar giros infinitos
        rb.maxAngularVelocity = 5f;
    }

    void FixedUpdate()
    {
        // Input
        float moveInput = Input.GetAxis("Vertical");   // W/S o ↑↓
        float turnInput = Input.GetAxis("Horizontal"); // A/D o ←→

        // Movimiento hacia adelante/atrás
        if (rb.linearVelocity.magnitude < maxSpeed || moveInput < 0)
        {
            rb.AddForce(transform.forward * moveInput * moveForce);
        }

        // Rotación más estable
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            Quaternion turnOffset = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * turnOffset);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // Frena cualquier giro acumulado tras una colisión
        rb.angularVelocity = Vector3.zero;
    }
}

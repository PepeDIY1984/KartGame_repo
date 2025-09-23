using UnityEngine;

public class PlayerMovementWrapper : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float deceleration = 8f; // cuanto mayor, más rápido se frena

    private CharacterController controller;
    public Vector3 currentVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Entrada de movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDir.magnitude > 0.1f)
        {
            // Si hay input, ajusta velocidad hacia esa dirección
            currentVelocity = inputDir * moveSpeed;
        }
        else
        {
            // Si NO hay input, aplica desaceleración progresiva
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Aplicar movimiento con CharacterController
        controller.Move(currentVelocity * Time.deltaTime);
    }
}

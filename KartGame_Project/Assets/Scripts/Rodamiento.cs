using UnityEngine;

public class Rodamiento : MonoBehaviour


{

    public Rigidbody playerRb;
    public float wheelRadius = 0.5f; // Radio de la rueda en metros
    public Vector3 rotationAxis = Vector3.right; // Eje de rotaci�n de la rueda
    

    void Start()
    {
        if (playerRb == null)
        {
            Debug.LogError("El padre debe tener un Rigidbody para calcular la velocidad.");
            enabled = false;
        }
    }

    void Update()
    {
        if (playerRb == null) return;

        // Obtener velocidad del rigidbody del padre
        Vector3 velocity = playerRb.linearVelocity;

        // Calcular componente de velocidad en la direcci�n hacia adelante del padre
        float forwardSpeed = Vector3.Dot(velocity, playerRb.transform.forward);

        // Calcular cu�nto deber�a girar la rueda este frame
        float angularSpeed = (forwardSpeed / wheelRadius) * Mathf.Rad2Deg; // en grados/segundo
        float angle = angularSpeed * Time.deltaTime;

        // Rotar la rueda
        transform.Rotate(rotationAxis, angle, Space.Self);
    }
}


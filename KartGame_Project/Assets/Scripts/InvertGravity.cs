using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InvertGravity : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Asegurarse de que la gravedad está activada
    }

    void Update()
    {
        // Pulsar espacio para invertir la gravedad
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScaleInvert();
        }
    }
}

public static class RigidbodyExtensions
{
    public static void gravityScaleInvert(this Rigidbody rb)
    {
        // Cambia la dirección de la gravedad multiplicando por -1
        Physics.gravity = -Physics.gravity;
    }
}

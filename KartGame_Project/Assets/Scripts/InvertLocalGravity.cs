using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InvertLocalGravity : MonoBehaviour
{
    private Rigidbody rb;
    public bool invertGravity = false;     // Activa o desactiva la gravedad invertida
    public float gravityScale = 1.0f;      // Multiplicador de fuerza de gravedad

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivamos la gravedad global de Unity
    }

    void FixedUpdate()
    {
        // Definir la dirección de la gravedad local (normal o invertida)
        Vector3 customGravity = invertGravity ? Vector3.up : Vector3.down;

        // Aplicar fuerza simulando la gravedad solo a este Rigidbody
        rb.AddForce(customGravity * 9.81f * gravityScale, ForceMode.Acceleration);
    }

    void Update()
    {
        // Pulsar espacio para alternar entre gravedad normal e invertida
        if (Input.GetKeyDown(KeyCode.Space))
        {
            invertGravity = !invertGravity;
        }
    }
}
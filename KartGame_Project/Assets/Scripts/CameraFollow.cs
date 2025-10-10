using UnityEngine;

public class CarCameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;         // El coche (Player)

    [Header("Offsets")]
    public Vector3 offset = new Vector3(0, 5, -10); // Altura y distancia respecto al coche
    public float followSpeed = 5f;   // Velocidad de seguimiento suave
    public float rotationSmooth = 5f; // Suavizado de rotación

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada detrás del coche
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Interpolamos suavemente hacia esa posición
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Rotación suave para mirar al coche
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmooth * Time.deltaTime);
    }
}

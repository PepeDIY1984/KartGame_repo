using UnityEngine;

public class CarCameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // El coche (Player)

    [Header("Offsets")]
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Posición relativa al coche

    [Header("Smoothing (frame-rate independent)")]
    [Tooltip("0 = muy suave/lento, 10-20 = rápido, 30+ = muy pegado")]
    public float positionSharpness = 12f;  // Controla la rapidez del seguimiento de posición
    public float rotationSharpness = 10f;  // Controla la rapidez del seguimiento de rotación

    [Header("Look-ahead")]
    [Tooltip("Distancia por delante del coche a la que mira la cámara")]
    public float lookAheadDistance = 4f;
    [Tooltip("Suavizado del punto de mira (segundos aprox). 0.03-0.1 ideal)")]
    public float lookPointSmoothTime = 0.06f;

    private Vector3 lookPointVelocity;
    private Vector3 smoothedLookPoint;

    void FixedUpdate()
    {
        if (!target) return;

        // --- 1) Punto de mira estable (delante del coche) --------------------
        Vector3 rawLookPoint = target.position + target.forward * lookAheadDistance;

        // Suavizamos el punto de mira (SmoothDamp elimina micro saltos)
        smoothedLookPoint = Vector3.SmoothDamp(
            smoothedLookPoint == Vector3.zero ? rawLookPoint : smoothedLookPoint,
            rawLookPoint,
            ref lookPointVelocity,
            Mathf.Max(0.0001f, lookPointSmoothTime)
        );

        // --- 2) Posición deseada de la cámara -------------------------------
        Vector3 desiredPos = target.TransformPoint(offset);

        // Suavizado exponencial basado en tiempo fijo de física
        float posT = 1f - Mathf.Exp(-positionSharpness * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredPos, posT);

        // --- 3) Rotación deseada hacia el punto de mira ---------------------
        Quaternion desiredRot = Quaternion.LookRotation(
            smoothedLookPoint - transform.position,
            Vector3.up
        );

        float rotT = 1f - Mathf.Exp(-rotationSharpness * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotT);
    }
}

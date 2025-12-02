using UnityEngine;

/// <summary>
/// Controla una cámara de minimapa en vista cenital (ortográfica).
/// - Sigue al target con suavizado independiente del framerate.
/// - Permite rotar el minimapa con el coche o mantenerlo fijo al norte.
/// - Zoom suave con límites (puedes mapear teclas o hacerlo vía UI).
/// Requiere: este script en un GameObject con Camera (marcada como Orthographic).
/// </summary>
[RequireComponent(typeof(Camera))]
public class MinimapCameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Altura y orientación")]
    [Tooltip("Altura de la cámara sobre el target (en unidades)")]
    public float height = 30f;

    [Tooltip("Si está activado, el minimapa gira con el coche. Si no, queda fijo (N arriba).")]
    public bool rotateWithTarget = true;

    [Header("Suavizado (independiente del framerate)")]
    [Tooltip("0 = muy suave, 10–20 = rápido, 30+ = muy pegado")]
    public float positionSharpness = 15f;
    public float rotationSharpness = 20f;
    public float zoomSharpness = 10f;

    [Header("Zoom")]
    [Tooltip("Tamaño ortográfico mínimo (más cerca)")]
    public float minOrthoSize = 12f;
    [Tooltip("Tamaño ortográfico máximo (más lejos)")]
    public float maxOrthoSize = 40f;
    [Tooltip("Tamaño ortográfico objetivo inicial")]
    public float targetOrthoSize = 20f;

    [Header("Controles opcionales")]
    [Tooltip("Tecla para hacer zoom in (opcional)")]
    public KeyCode zoomInKey = KeyCode.Equals;   // '='
    [Tooltip("Tecla para hacer zoom out (opcional)")]
    public KeyCode zoomOutKey = KeyCode.Minus;   // '-'
    [Tooltip("Incremento de zoom por pulsación")]
    public float zoomStep = 3f;

    [Header("Capas (opcional)")]
    [Tooltip("Culling mask para el minimapa (ej: solo 'Minimap' y 'Ground')")]
    public LayerMask minimapCullingMask = ~0; // por defecto todo

    private Camera _cam;

    void Awake()
    {
        _cam = GetComponent<Camera>();
        _cam.orthographic = true;
        _cam.cullingMask = minimapCullingMask;
        _cam.orthographicSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);
    }

    void Update()
    {
        // Controles de zoom (opcionales)
        if (Input.GetKeyDown(zoomInKey))
            targetOrthoSize -= zoomStep;
        if (Input.GetKeyDown(zoomOutKey))
            targetOrthoSize += zoomStep;

        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);
    }

    void LateUpdate()
    {
        if (!target) return;

        // --- POSICIÓN (suavizado exponencial, indep. framerate) ---------------
        Vector3 desiredPos = target.position + Vector3.up * height;
        float posT = 1f - Mathf.Exp(-positionSharpness * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredPos, posT);

        // --- ROTACIÓN ---------------------------------------------------------
        Quaternion desiredRot;
        if (rotateWithTarget)
        {
            // Yaw del target; cámara mira hacia abajo
            desiredRot = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
        else
        {
            // Norte arriba: siempre mira -Y con yaw = 0
            desiredRot = Quaternion.Euler(90f, 0f, 0f);
        }

        float rotT = 1f - Mathf.Exp(-rotationSharpness * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotT);

        // --- ZOOM -------------------------------------------------------------
        float zT = 1f - Mathf.Exp(-zoomSharpness * Time.deltaTime);
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, targetOrthoSize, zT);
    }

    /// <summary>
    /// Llamar desde UI deslizadores/botones si quieres controlar el zoom externamente.
    /// </summary>
    public void SetTargetOrthoSize(float newSize)
    {
        targetOrthoSize = Mathf.Clamp(newSize, minOrthoSize, maxOrthoSize);
    }

    /// <summary>
    /// Alternar si el minimapa rota con el coche (para un botón de UI).
    /// </summary>
    public void ToggleRotateWithTarget(bool value)
    {
        rotateWithTarget = value;
    }
}

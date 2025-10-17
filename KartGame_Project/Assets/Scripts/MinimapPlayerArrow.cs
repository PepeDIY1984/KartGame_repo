using UnityEngine;
using UnityEngine.UI;

public class MinimapPlayerArrow : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;               // Asigna el player/coche
    public MinimapCameraController minimapCamera; // Cámara del minimapa

    [Header("Opciones")]
    [Tooltip("Si el minimapa gira con el jugador, la flecha queda fija apuntando hacia arriba.")]
    public bool keepArrowFixedWhenMapRotates = true;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        if (minimapCamera != null && minimapCamera.rotateWithTarget && keepArrowFixedWhenMapRotates)
        {
            // El mapa rota con el coche, la flecha permanece arriba
            rectTransform.localRotation = Quaternion.identity;
        }
        else
        {
            // El mapa está fijo, la flecha rota según el coche
            float playerYaw = player.eulerAngles.y;
            rectTransform.localRotation = Quaternion.Euler(0f, 0f, -playerYaw);
        }
    }
}

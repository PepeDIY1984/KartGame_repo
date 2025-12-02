using UnityEngine;
using TMPro;   // Importante para usar TextMeshProUGUI
using System.Collections;

public class GameManagerCarrera : MonoBehaviour
{
    [Header("Referencias")]
    public TextMeshProUGUI messageText;   // Referencia a un TMP Text en el Canvas

    [Header("Duraciones de mensajes")]
    public float messageDuration = 2f;        // Tiempo para mensajes normales
    public float goalMessageDuration = 5f;    // Tiempo especial para la meta

    private int currentCheckpoint = 0;        // Checkpoint actual que debe atravesar
    private Coroutine currentMessageRoutine;

    void Start()
    {
        ShowMessage("¡Empieza la carrera! Pasa por el Checkpoint 1", messageDuration);
    }

    // Llamado desde los triggers
    public void CheckpointReached(int checkpointNumber)
    {
        if (checkpointNumber == currentCheckpoint + 1)
        {
            currentCheckpoint = checkpointNumber;
            ShowMessage("Checkpoint " + checkpointNumber + " alcanzado ✅", messageDuration);
        }
        else
        {
            ShowMessage("¡No es el checkpoint correcto! Debes ir en orden.", messageDuration);
        }
    }

    public void GoalReached()
    {
        if (currentCheckpoint == 3) // todos los checkpoints completados
        {
            ShowMessage("🏁 ¡Has llegado a la meta tras completar todos los checkpoints!", goalMessageDuration);
        }
        else
        {
            ShowMessage("No puedes terminar todavía, faltan checkpoints.", messageDuration);
        }
    }

    // Mostrar mensajes en la UI y ocultarlos después de unos segundos
    private void ShowMessage(string msg, float duration)
    {
        if (messageText != null)
        {
            messageText.text = msg;

            // Cancelar corrutina anterior si la hay
            if (currentMessageRoutine != null)
                StopCoroutine(currentMessageRoutine);

            // Iniciar nueva corrutina con duración específica
            currentMessageRoutine = StartCoroutine(ClearMessageAfterDelay(duration));
        }
        Debug.Log(msg);
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
        currentMessageRoutine = null;
    }
}

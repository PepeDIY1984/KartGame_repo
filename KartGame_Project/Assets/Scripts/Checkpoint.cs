using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber = 0; // 1, 2, 3... meta = 99
    public GameManagerCarrera gameManager;  // Se asigna en el Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (checkpointNumber ==4)
            {
                gameManager.GoalReached();
            }
            else
            {
                gameManager.CheckpointReached(checkpointNumber);
            }
        }
    }
}

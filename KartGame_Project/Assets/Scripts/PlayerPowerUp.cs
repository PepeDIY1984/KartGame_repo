using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hola");
        if (other.CompareTag("PowerUp")) {
            transform.localScale= new Vector3(3, 3, 3);
            Destroy(other.gameObject);
            Invoke("changeScale", 3);
        }
    }

    void changeScale()
    {
        transform.localScale = new Vector3(1,1, 1);

    }

}
